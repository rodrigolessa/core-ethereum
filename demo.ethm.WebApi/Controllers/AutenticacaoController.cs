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
using demo.ethm.Aplicacao.Models;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Autenticação dos usuários na aplicação utilizando e-mail e senha
    /// </summary>
    [AllowAnonymous]
    [Route("api/Autenticacao")]
    public class AutenticacaoController : DemoControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MainContext _contexto;

        /// <summary>
        /// Contrutor da autenticação
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="contexto"></param>
        public AutenticacaoController(IConfiguration configuration, MainContext contexto)
        {
            _configuration = configuration;
            _contexto = contexto;
        }

        /// <summary>
        /// Receber a requisição de login/e-mail e senha para validar o cadastro do usuário. Retorna um token válido para persistência do usuário durante a navegação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Entrar")]
        public ResponseBase<UsuarioDTO> Entrar([FromBody]EntradaRequest request)
        {
             using (AutenticacaoAppService appService = new AutenticacaoAppService(_configuration, _contexto))
             {
                ResponseBase<UsuarioDTO> br = appService.ValidarEntrada(request.Email, request.Senha);
                br.TempoLevado = appService.swTempoRequisicao.Elapsed;
                br.Autorizado = br.Mensagens.Count == 0;
                return br;

            }
        }

        /// <summary>
        /// Verifica se o token informado no header da requisição é válido
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Validar")]
        public ResponseBase<UsuarioDTO> Validar()
        {
            using (UsuarioAppService appService = new UsuarioAppService(_configuration, _contexto))
            {
                return ObterUsuarioRequest();
            }
        }
    }
}
