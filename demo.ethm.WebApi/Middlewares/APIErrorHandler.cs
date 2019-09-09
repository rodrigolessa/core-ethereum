using demo.ethm.Aplicacao.Models.Response;
using demo.ethm.Infraestrutura.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace demo.ethm.WebApi.Middlewares
{

    // TODO: Podemos substituir a implementação simplesmente injetando o HttpContext como Singleton 
    // e criando uma classe para representar o usuário que vai acessar as informações do Contexto
    // , depois injetar a classe do usuário logado nos métodos que precisar de validação ou autenticação

    /// <summary>
    /// Middleware para controle de erro na Api
    /// </summary>
    public class APIErrorHandler
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Construtor de classe. O Delegate next é nativo do .Net Core dentro do contexto de um middleware
        /// </summary>
        /// <param name="next"></param>
        public APIErrorHandler(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Obtem dados do contexto web e monta a stack do erro
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    return;
                }

                UsuarioResponse usuario = null;
                var usuarioObj = context.Request.HttpContext.Items.Where(c => c.Key.ToString() == "usuario").Select(c => c.Value).FirstOrDefault();
                if (usuarioObj != null)
                {
                    usuario = (UsuarioResponse)usuarioObj;
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("****** <b>ERRO</b> ******");
                sb.AppendLine(string.Empty);
                sb.AppendLine("<b>Data/Hora: </b>" + DateTime.Now.ToString());


                if (usuario != null && !string.IsNullOrWhiteSpace(usuario.Email))
                {
                    sb.AppendLine("<b>Nome do usuário: </b>" + usuario.Email);
                    sb.AppendLine("<b>Token do usuário: </b>" + context.Request.Headers[HeaderNames.Authorization].ToString());
                }

                sb.AppendLine("<b>Tipo de método usado: </b>" + context.Request.Method);
                sb.AppendLine("<b>Url do método usado: </b>" + (context.Request.Path.HasValue ? context.Request.Path.Value : ""));
                sb.AppendLine("<b>Query do método usado: </b>" + (context.Request.QueryString.HasValue ? context.Request.QueryString.Value : ""));
                sb.AppendLine("<b>Url completa usada: </b>" + (context.Request.PathBase.HasValue ? context.Request.PathBase.Value : ""));

                Regex OS = new Regex(@"/Mobile|iP(hone|od|ad)|Android|BlackBerry|IEMobile|Kindle|NetFront|Silk-Accelerated|(hpw|web)OS|Fennec|Minimo|Opera M(obi|ini)|Blazer|Dolfin|Dolphin|Skyfire|Zune/", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Regex device = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string device_info = string.Empty;

                if (OS.IsMatch(context.Request.Headers[HeaderNames.UserAgent].ToString()))
                {
                    device_info = OS.Match(context.Request.Headers[HeaderNames.UserAgent].ToString()).Groups[0].Value;
                }
                if (device.IsMatch(context.Request.Headers[HeaderNames.UserAgent].ToString().Substring(0, 4)))
                {
                    device_info += " " + device.Match(context.Request.Headers[HeaderNames.UserAgent].ToString()).Groups[0].Value;
                }
                if (!string.IsNullOrWhiteSpace(device_info))
                {
                    sb.AppendLine("<b>Dispositivo usado: </b>" + device_info);
                }

                sb.AppendLine("<b>User-Agent usado: </b>" + context.Request.Headers[HeaderNames.UserAgent].ToString());

                if (context.Request.Headers[HeaderNames.Authorization].ToString() != "")
                {
                    sb.AppendLine("<b>Autorization do método usado: </b>" + context.Request.Headers[HeaderNames.Authorization].ToString());
                }

                string corpo = string.Empty;

                sb.AppendLine(string.Empty);

                var textoRequest = sb.ToString();

                var erro = LogarErro(ex);

                EmailHelper email = new EmailHelper("smtp@demo.ethm.com", false, 25, "team@demo.ethm.com.br", "");
                email.Enviar("team@demo.ethm.com.br", "rodrigolsr@gmail.com", "WebApi - ERRO", textoRequest.Replace("\r\n", "<br />").Replace("&", "&amp;") + erro.Replace("\r\n", "<br />").Replace("&", "&amp;"));

                //context.Response.Clear();

                ResponseBase<bool> resposta = new ResponseBase<bool>()
                {
                    Autorizado = true,
                    Sucesso = false,
                    Objeto = false,
                    Mensagens = new List<string>() { "Erro interno, entre em contato para mais detalhes." }
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(resposta));

                return;
            }
        }

        /// <summary>
        /// Enviar por e-mail o stack de erro não controlado
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public string LogarErro(Exception exc)
        {
            StringBuilder sb = new StringBuilder();

            if (exc.InnerException != null && exc.InnerException.InnerException != null)
            {
                sb.AppendLine("****** Exceção interna [2] ******");
                sb.AppendLine("");
                sb.AppendLine("Tipo da exceção interna[2]: ");
                sb.AppendLine(exc.InnerException.InnerException.GetType().ToString());
                sb.AppendLine("");
                sb.AppendLine("Exceção interna mensagem[2]: ");
                sb.AppendLine("<b>" + exc.InnerException.InnerException.Message + "</b>");
                sb.AppendLine("");
                sb.AppendLine("Fonte interna[2]: ");
                sb.AppendLine(exc.InnerException.InnerException.Source);

                if (exc.InnerException.InnerException.StackTrace != null)
                {
                    StackTrace st = new StackTrace(exc.InnerException.InnerException, true);
                    string fileName = string.Empty;
                    string filePath = string.Empty;
                    string lineNumber = string.Empty;
                    string colNumber = string.Empty;
                    string methodName = string.Empty;
                    string codeLine = string.Empty;

                    foreach (var item in st.GetFrames().ToList())
                    {
                        if (item.GetFileLineNumber() > 0)
                        {
                            filePath = item.GetFileName();
                            fileName = Path.GetFileName(filePath);
                            methodName = item.GetMethod().Name;
                            lineNumber = item.GetFileLineNumber().ToString();
                            colNumber = item.GetFileColumnNumber().ToString();

                            if (File.Exists(filePath))
                            {
                                var codeFile = File.ReadAllLines(filePath).ToList();
                                var line = item.GetFileLineNumber();

                                if (codeFile.Count > line)
                                {
                                    codeLine = codeFile[line - 1].Trim();
                                }
                            }

                            break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        sb.AppendLine("");
                        sb.AppendLine("No Arquivo: ");
                        sb.AppendLine(fileName);

                        if (!string.IsNullOrWhiteSpace(methodName))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("No método: ");
                            sb.AppendLine(methodName);
                        }

                        if (!string.IsNullOrWhiteSpace(lineNumber))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("Na linha: ");
                            sb.AppendLine(lineNumber);
                        }

                        if (!string.IsNullOrWhiteSpace(colNumber))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("Na coluna: ");
                            sb.AppendLine(colNumber);
                        }

                        if (!string.IsNullOrWhiteSpace(codeLine))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("Código de erro: ");
                            sb.AppendLine(codeLine);
                        }
                    }

                    sb.AppendLine("");
                    sb.AppendLine("Rastreamento de pilha interna[2]: ");
                    sb.AppendLine(exc.InnerException.InnerException.StackTrace);
                }

                sb.AppendLine("");
                sb.AppendLine("****** Fim Exceção interna [2] ******");
                sb.AppendLine("");
            }

            if (exc.InnerException != null)
            {
                sb.AppendLine("****** Exceção interna ******");
                sb.AppendLine("");
                sb.AppendLine("Tipo da exceção interna: ");
                sb.AppendLine(exc.InnerException.GetType().ToString());
                sb.AppendLine("");
                sb.AppendLine("Exceção interna mensagem: ");
                sb.AppendLine("<b>" + exc.InnerException.Message + "</b>");
                sb.AppendLine("");
                sb.AppendLine("Fonte interna: ");
                sb.AppendLine(exc.InnerException.Source);

                if (exc.InnerException.StackTrace != null)
                {
                    StackTrace st = new StackTrace(exc.InnerException, true);
                    string fileName = string.Empty;
                    string filePath = string.Empty;
                    string lineNumber = string.Empty;
                    string colNumber = string.Empty;
                    string methodName = string.Empty;
                    string codeLine = string.Empty;

                    foreach (var item in st.GetFrames().ToList())
                    {
                        if (item.GetFileLineNumber() > 0)
                        {
                            filePath = item.GetFileName();
                            fileName = Path.GetFileName(filePath);
                            methodName = item.GetMethod().Name;
                            lineNumber = item.GetFileLineNumber().ToString();
                            colNumber = item.GetFileColumnNumber().ToString();

                            if (File.Exists(filePath))
                            {
                                var codeFile = File.ReadAllLines(filePath).ToList();
                                var line = item.GetFileLineNumber();

                                if (codeFile.Count > line)
                                {
                                    codeLine = codeFile[line - 1].Trim();
                                }
                            }

                            break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        sb.AppendLine("");
                        sb.AppendLine("No Arquivo: ");
                        sb.AppendLine(fileName);

                        if (!string.IsNullOrWhiteSpace(methodName))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("No método: ");
                            sb.AppendLine(methodName);
                        }

                        if (!string.IsNullOrWhiteSpace(lineNumber))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("Na linha: ");
                            sb.AppendLine(lineNumber);
                        }

                        if (!string.IsNullOrWhiteSpace(colNumber))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("Na coluna: ");
                            sb.AppendLine(colNumber);
                        }

                        if (!string.IsNullOrWhiteSpace(codeLine))
                        {
                            sb.AppendLine("");
                            sb.AppendLine("Código de erro: ");
                            sb.AppendLine(codeLine);
                        }
                    }

                    sb.AppendLine("");
                    sb.AppendLine("Rastreamento de pilha interna: ");
                    sb.AppendLine(exc.InnerException.StackTrace);
                }

                sb.AppendLine("");
                sb.AppendLine("****** Fim Exceção interna ******");
                sb.AppendLine("");
            }

            sb.AppendLine("****** Exceção ******");
            sb.AppendLine("");
            sb.AppendLine("Tipo da exceção: ");
            sb.AppendLine(exc.GetType().ToString());
            sb.AppendLine("");
            sb.AppendLine("Exceção mensagem: ");
            sb.AppendLine("<b>" + exc.Message + "</b>");

            if (exc.StackTrace != null)
            {
                StackTrace st = new StackTrace(exc, true);
                string fileName = string.Empty;
                string filePath = string.Empty;
                string lineNumber = string.Empty;
                string colNumber = string.Empty;
                string methodName = string.Empty;
                string codeLine = string.Empty;

                foreach (var item in st.GetFrames().ToList())
                {
                    if (item.GetFileLineNumber() > 0)
                    {
                        filePath = item.GetFileName();
                        fileName = Path.GetFileName(filePath);
                        methodName = item.GetMethod().Name;
                        lineNumber = item.GetFileLineNumber().ToString();
                        colNumber = item.GetFileColumnNumber().ToString();

                        if (File.Exists(filePath))
                        {
                            var codeFile = File.ReadAllLines(filePath).ToList();
                            var line = item.GetFileLineNumber();

                            if (codeFile.Count > line)
                            {
                                codeLine = codeFile[line - 1].Trim();
                            }
                        }

                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    sb.AppendLine("");
                    sb.AppendLine("No Arquivo: ");
                    sb.AppendLine(fileName);

                    if (!string.IsNullOrWhiteSpace(methodName))
                    {
                        sb.AppendLine("");
                        sb.AppendLine("No método: ");
                        sb.AppendLine(methodName);
                    }

                    if (!string.IsNullOrWhiteSpace(lineNumber))
                    {
                        sb.AppendLine("");
                        sb.AppendLine("Na linha: ");
                        sb.AppendLine(lineNumber);
                    }

                    if (!string.IsNullOrWhiteSpace(colNumber))
                    {
                        sb.AppendLine("");
                        sb.AppendLine("Na coluna: ");
                        sb.AppendLine(colNumber);
                    }

                    if (!string.IsNullOrWhiteSpace(codeLine))
                    {
                        sb.AppendLine("");
                        sb.AppendLine("Código de erro: ");
                        sb.AppendLine(codeLine);
                    }
                }

                sb.AppendLine("");
                sb.AppendLine("Rastreamento de pilha: ");
                sb.AppendLine(exc.StackTrace);
            }

            sb.AppendLine("");
            sb.AppendLine("****** Fim Exceção ******");

            return sb.ToString();
        }
    }

    /// <summary>
    /// Extension para Api de controle de erro
    /// </summary>
    public static class APIErrorHandlerExtensions
    {
        /// <summary>
        /// Middleware para Api de Erro
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAPIErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIErrorHandler>();
        }
    }
}
