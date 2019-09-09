using demo.ethm.Aplicacao;
using demo.ethm.Aplicacao.EntityExtensions;
using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Request;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Classe para manter os usuários da aplicação
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : DemoControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MainContext _contexto;

        /// <summary>
        /// Contrutor da classe
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="contexto"></param>
        public UsuariosController(IConfiguration configuration, MainContext contexto)
        {
            _configuration = configuration;
            _contexto = contexto;
        }

        /// <summary>
        /// Obter dados do usuário logado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ResponseBase<UsuarioDTO> GetUsuario(int id)
        {
            using (UsuarioAppService appService = new UsuarioAppService(_configuration, _contexto))
            {
                // TODO: Boas práticas: Implementar injeção de dependencia do HttpContextAccessor como singleton no container 
                // e manter a responsabilidade de validação e autenticação para a camada de "Application"
                // https://www.eduardopires.net.br/2016/12/asp-net-core-obtendo-o-usuario-logado-em-qualquer-camada/
                ResponseBase<UsuarioDTO> dto = ObterUsuarioRequest();

                if (!dto.Autorizado)
                {
                    dto.TempoLevado = appService.swTempoRequisicao.Elapsed;
                    return dto;
                }

                appService.UsuarioLogado = dto.Objeto;
                var br = appService.Obter(id);
                br.Autorizado = true;
                br.TempoLevado = appService.swTempoRequisicao.Elapsed;

                return br;
            }
        }

        /// <summary>
        /// Alterar dados do usuário, exceto Foto, e-mail e senha
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Route("AlterarPerfil")]
        public ResponseBase<bool> AlterarPerfil(int id, UsuarioRequest request)
        {
            ResponseBase<bool> br = new ResponseBase<bool>();

            #region Validação

            if (id == 0)
            {
                br.Mensagens.Add(BadRequest().ToString());
                return br;
            }

            #endregion

            using (UsuarioAppService appService = new UsuarioAppService(_configuration, _contexto))
            {
                ResponseBase<UsuarioDTO> dto = ObterUsuarioRequest();

                if (dto.Autorizado)
                {
                    appService.UsuarioLogado = dto.Objeto;
                    br = appService.Editar(id, request);
                }
                else
                {
                    br.Mensagens = dto.Mensagens;
                }

                br.Autorizado = dto.Autorizado;
                br.TempoLevado = appService.swTempoRequisicao.Elapsed;

                return br;
            }
        }

        /// <summary>
        /// Alterar senha do usuário. Não exige confirmação por e-mail.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AlterarSenha")]
        public ResponseBase<bool> AlterarSenha(int id, AlterarSenhaRequest request)
        {
            ResponseBase<bool> br = new ResponseBase<bool>();

            #region Validação

            if (id == 0)
            {
                br.Mensagens.Add(BadRequest().ToString());
                return br;
            }

            #endregion

            using (UsuarioAppService appService = new UsuarioAppService(_configuration, _contexto))
            {
                ResponseBase<UsuarioDTO> dto = ObterUsuarioRequest();

                if (dto.Autorizado)
                {
                    appService.UsuarioLogado = dto.Objeto;
                    // TODO: Senha
                    br = appService.AlterarSenha(id, request);
                }
                else
                {
                    br.Mensagens = dto.Mensagens;
                }

                br.Autorizado = dto.Autorizado;
                br.TempoLevado = appService.swTempoRequisicao.Elapsed;

                return br;
            }
        }
    }
}
