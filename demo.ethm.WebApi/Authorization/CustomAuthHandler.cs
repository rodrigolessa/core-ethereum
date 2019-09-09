using demo.ethm.Aplicacao;
using demo.ethm.Aplicacao.Models;
using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace demo.ethm.WebApi.Authorization
{
    internal class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        private const string HttpContextName = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";
        private readonly IConfiguration _configuration;
        private readonly MainContext _contexto;

        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration,
            MainContext contexto
        ) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
            _contexto = contexto;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            ResponseBase<UsuarioDTO> br = new ResponseBase<UsuarioDTO>();
            string scheme = string.Empty;
            string token = string.Empty;

            if (!string.IsNullOrWhiteSpace(Request.Headers[HeaderNames.Authorization]))
            {
                var authToken = Request.Headers[HeaderNames.Authorization].ToString();

                if (authToken.Split(' ').Length > 1)
                {
                    scheme = authToken.Split(' ')[0].Trim();
                    token = authToken.Split(' ')[1].Trim();
                }
                else
                {
                    token = authToken.Trim();
                }
            }

            if (!string.IsNullOrWhiteSpace(token))
            {
                using (AutenticacaoAppService _autenticacaoAppService = new AutenticacaoAppService(_configuration, _contexto))
                {
                    //_autenticacaoAppService.Codigo = this.Request.Headers["codigo"];
                    br = _autenticacaoAppService.ValidarToken(token);
                    br.TempoLevado = _autenticacaoAppService.swTempoRequisicao.Elapsed;
                }
            }
            else
            {
                br.Mensagens.Add("Usuário não encontrado!");
            }

            br.Autorizado = br.Mensagens.Count == 0;

            Request.HttpContext.Items.Add("usuario", br);

            var user = new GenericPrincipal(new GenericIdentity("User"), null);
            var ticket = new AuthenticationTicket(user, new AuthenticationProperties(), CustomAuthOptions.DefaultSchemeName);

            await Task.Delay(0);

            return AuthenticateResult.Success(ticket);
        }

        protected string GetHostAdress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContextName))
            {
                dynamic ctx = request.Properties[HttpContextName];

                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];

                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic ctx = request.Properties[OwinContext];

               if (ctx != null)
                {
                    return ctx.Request.RemoteIpAddress;
                }
            }

            return "0.0.0.0";
        }

        protected string GetHostName(HttpRequestMessage request)
        {
            if (request != null)
            {
                if (request.Properties.ContainsKey(HttpContextName))
                {
                    dynamic ctx = request.Properties[HttpContextName];

                    if (ctx != null)
                    {
                        return ctx.Request.UserHostName;
                    }
                }

                return "Não encontrado.";
            }
            else
            {
                return "Não encontrado.";
            }
        }
    }
}