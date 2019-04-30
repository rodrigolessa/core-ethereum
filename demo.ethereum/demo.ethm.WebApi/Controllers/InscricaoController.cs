using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using demo.ethm.Infraestrutura;
using demo.ethm.Infraestrutura.Persistencia;
using demo.ethm.Aplicacao.Models.Request;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Aplicacao;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Novas inscrições para o aplicativo
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InscricaoController : DemoControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MainContext _contexto;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="contexto"></param>
        public InscricaoController(IConfiguration configuration, MainContext contexto)
        {
            _configuration = configuration;
            _contexto = contexto;
        }

        /// <summary>
        /// Informando e-mail e senha para realizar o cadastro de um usuário na aplicação. Exige validação por e-mail.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase<bool> PostInscricao([FromBody]InscricaoRequest request)
        {
            using (UsuarioAppService appService = new UsuarioAppService(_configuration, _contexto))
            {
                var br = appService.Inscrever(request);
                br.TempoLevado = appService.swTempoRequisicao.Elapsed;
                br.Autorizado = br.Mensagens.Count == 0;
                return br;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("ConfirmarEmail")]
        public ResponseBase<bool> ConfirmarEmail([FromBody]string token)
        {
            using (UsuarioAppService appService = new UsuarioAppService(_configuration, _contexto))
            {
                // TODO: Confirmação
                var br = appService.ConfirmarEmail(token);
                br.TempoLevado = appService.swTempoRequisicao.Elapsed;
                br.Autorizado = br.Mensagens.Count == 0;
                return br;
            }
        }
    }
}