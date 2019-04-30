using demo.ethm.Aplicacao.EntityExtensions;
using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Infraestrutura.Helpers;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Aplicacao
{
    public class AutenticacaoAppService : AppServiceBase
    {
        private readonly IConfiguration _configuration;
        private readonly MainContext _contexto;

        public AutenticacaoAppService()
        {
            swTempoRequisicao.Start();
        }

        public AutenticacaoAppService(IConfiguration configuration, MainContext contexto)
        {
            swTempoRequisicao.Start();

            _configuration = configuration;
            _contexto = contexto;
        }

        public ResponseBase<UsuarioDTO> ValidarEntrada(string email, string senha)
        {
            ResponseBase<UsuarioDTO> br = new ResponseBase<UsuarioDTO>();

            #region validação Inicial

            if (string.IsNullOrWhiteSpace(email))
            {
                br.Mensagens.Add("Informe o e-mail.");
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                br.Mensagens.Add("Informe a senha.");
            }

            if (br.Mensagens.Count > 0)
            {
                return br;
            }

            #endregion

            var usuario = _contexto.Usuario.FirstOrDefault(u => u.Email == email);
            if (usuario == null)
            {
                br.Mensagens.Add("Usuário ou senha inválidos");
                return br;
            }

            if (!usuario.ValidarSenha(senha))
            {
                br.Mensagens.Add("Usuário ou senha inválidos");
                return br;
            }

            if (usuario.Situacao != SituacaoDoUsuario.Liberado)
            {
                br.Mensagens.Add("Acesse seu e-mail para confirmar o cadastramento");
                return br;
            }

            br.Objeto = usuario.TraduzirParaDTO();

            UsuarioToken tk = new UsuarioToken()
            {
                IdUsuario = usuario.Id,
                Ativo = 1,
                DataDeInclusao = DateTime.Now,
                Token = CryptoHelper.GerarToken(usuario.Nome)
            };

            _contexto.UsuarioToken.Add(tk);
            _contexto.SaveChanges();

            br.Objeto.Token = tk.Token;

            return br;
        }

        public ResponseBase<UsuarioDTO> ValidarToken(string token)
        {
            ResponseBase<UsuarioDTO> br = new ResponseBase<UsuarioDTO>();

            #region validação

            if (string.IsNullOrWhiteSpace(token))
            {
                br.Mensagens.Add("Token não informado.");
            }

            if (br.Mensagens.Count > 0)
            {
                return br;
            }

            #endregion

            var tkAtivo = _contexto.UsuarioToken.FirstOrDefault(c => c.Token == token && c.Ativo == 1);
            if (tkAtivo == null)
            {
                br.Mensagens.Add("Token inválido.");
                return br;
            }

            var usuario = _contexto.Usuario.FirstOrDefault(c => c.Id == tkAtivo.IdUsuario && !c.DataDaExclusao.HasValue);
            if (usuario == null)
            {
                br.Mensagens.Add("Usuário não encontrado.");
                return br;
            }

            br.Objeto = usuario.TraduzirParaDTO();
            br.Objeto.Token = tkAtivo.Token;

            return br;
        }
    }
}
