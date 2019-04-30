using demo.ethm.Aplicacao.EntityExtensions;
using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Request;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura;
using demo.ethm.Infraestrutura.Helpers;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace demo.ethm.Aplicacao
{
    public class UsuarioAppService : AppServiceBase
    {
        private readonly IConfiguration _configuration;
        private readonly MainContext _contexto;

        public UsuarioAppService()
        {
            swTempoRequisicao.Start();
        }

        public UsuarioAppService(IConfiguration configuration, MainContext contexto)
        {
            swTempoRequisicao.Start();

            _configuration = configuration;
            _contexto = contexto;
        }

        public ResponseBase<UsuarioDTO> Obter(int id)
        {
            ResponseBase<UsuarioDTO> br = new ResponseBase<UsuarioDTO>();

            if (UsuarioLogado.Id != id)
            {
                br.Mensagens.Add("Não encontrado.");
                return br;
            }

            var usuario = _contexto.Usuario.Find(id);
            if (usuario == null)
            {
                br.Mensagens.Add("Não encontrado.");
                return br;
            }

            br.Objeto = usuario.TraduzirParaDTO();

            return br;
        }

        public ResponseBase<List<UsuarioDTO>> Listar()
        {
            throw new NotImplementedException();
        }

        public ResponseBase<bool> Inscrever(InscricaoRequest request)
        {
            ResponseBase<bool> br = new ResponseBase<bool>();

            var usuario = new Usuario(request.Nome, request.Senha, request.ConfirmacaoDeSenha, request.Email, request.Telefone)
            {
                Idioma = _contexto.Idioma.FirstOrDefault(i => i.Id == request.Idioma)
            };

            if (request.Tipo.HasValue)
            {
                usuario.Tipo = (TipoDeUsuario)request.Tipo;
            }

            if (_contexto.Usuario.Where(x => x.Email == request.Email).Count() > 0)
            {
                br.Mensagens.Add("Já existe um usuário cadastrado com este e-mail.");
            }

            var validationContext = new ValidationContext(usuario, null, null);
            var validationResults = ((IValidatableObject)usuario).Validate(validationContext);
            if (validationResults.Count() > 0)
            {
                foreach (ValidationResult v in validationResults)
                {
                    br.Mensagens.Add(v.ErrorMessage);
                }
            }

            if (br.Sucesso)
            {
                _contexto.Usuario.Add(usuario);
                if (_contexto.SaveChanges() > 0)
                {
                    // Enviar e-mail com token de validação do cadastro!
                    //http://app.demo.ethm.com/login?type=confirmed-email&token=asdhuasdhuashudashuduhasd

                    var template = @"<!DOCTYPE html><html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml""><head><meta charset=""utf-8"" /><title>demo.ethm</title></head><body><p>Clique <a href=""http://app.demo.ethm.com/login?type=confirmed-email&token={token}"">aqui</a> para confirmar seu cadastro em demo.ethm.com.</p></body></html>";

                    var assem = GetType().Assembly;

                    //var lista = assem.GetManifestResourceNames();

                    using (var stream = assem.GetManifestResourceStream("demo.ethm.Aplicacao.Resources.email_confirmar_cadastro_pt_br.html"))
                    {
                        template = new StreamReader(stream).ReadToEnd();
                    }

                    template.Replace("{token}", usuario.TokenConfirmacaoEmail);

                    // SMTP
                    // Server name: smtp-mail.outlook.com
                    // Port: 587
                    // Encryption method: TLS
                    // TODO: Move to configuration file
                    EmailHelper helper = new EmailHelper("smtp-mail.outlook.com", false, 25, "team@demo.ethm.com", "lcD!12345");

                    // TODO: Traduzir assunto do e-mail de confirmação de cadastro
                    br.Sucesso = helper.Enviar(usuario.Email, Constants.ArquivoDeConfiguracao.Default.EmailSuporte, "demo.ethm - Confirmação de Cadastro", template.Replace("\r\n", "<br />").Replace("&", "&amp;"));
                }
            }

            return br;
        }

        public ResponseBase<bool> ConfirmarEmail(string token)
        {
            ResponseBase<bool> br = new ResponseBase<bool>();

            var usuario = _contexto.Usuario.FirstOrDefault(i => i.TokenConfirmacaoEmail == token && i.Situacao == SituacaoDoUsuario.Aguardando);
            if (usuario == null)
            {
                br.Mensagens.Add("Não encontrado.");
                return br;
            }

            usuario.ConfirmarCadastro();

            _contexto.Entry(usuario).State = EntityState.Modified;
            _contexto.SaveChanges();

            return br;
        }

        public ResponseBase<bool> AlterarSenha(int id, AlterarSenhaRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<bool> Editar(int id, UsuarioRequest request)
        {
            ResponseBase<bool> br = new ResponseBase<bool>();

            var usuario = _contexto.Usuario.Find(id);
            if (usuario == null)
            {
                br.Mensagens.Add("Não encontrado.");
                return br;
            }

            usuario.SetNome(request.Nome);
            usuario.SetTelefone(request.Telefone);
            usuario.SetEndereco(
                new Endereco(
                    request.Logradouro,
                    request.Complemento,
                    request.Numero,
                    request.Bairro,
                    request.Municipio,
                    request.Uf.HasValue ? (Uf)request.Uf : 0,
                    request.OutroEstado,
                    new Cep(request.Cep, false)
                )
            );

            if (_contexto.Idioma.Any(x => x.Id == request.Idioma))
            {
                usuario.Idioma = _contexto.Idioma.FirstOrDefault(x => x.Id == request.Idioma);
            }

            if (request.Pais.HasValue)
            {
                if (_contexto.Pais.Any(x => x.Id == request.Pais))
                {
                    usuario.Pais = _contexto.Pais.FirstOrDefault(x => x.Id == request.Pais);
                }
            }

            if (request.Tipo.HasValue)
            {
                usuario.Tipo = (TipoDeUsuario)request.Tipo;
            }

            var validationContext = new ValidationContext(usuario, null, null);
            var validationResults = ((IValidatableObject)usuario).Validate(validationContext);
            if (validationResults.Count() > 0)
            {
                foreach (ValidationResult v in validationResults)
                {
                    br.Mensagens.Add(v.ErrorMessage);
                }
            }

            try
            {
                if (br.Sucesso)
                {
                    _contexto.Entry(usuario).State = EntityState.Modified;
                    _contexto.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    br.Mensagens.Add("Não encontrado.");
                }
                else
                {
                    br.Mensagens.Add("Ocorreu um erro não esperado.");
                }
            }

            return br;
        }

        private bool UsuarioExists(int id)
        {
            return _contexto.Usuario.Any(e => e.Id == id);
        }
    }
}
