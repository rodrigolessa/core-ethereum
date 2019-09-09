using demo.ethm.Aplicacao.Models.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace demo.ethm.WebApi.Middlewares
{
    /// <summary>
    /// Middleware for httpcontext ?
    /// </summary>
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Response ?
        /// </summary>
        /// <param name="next"></param>
        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Task
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();

            watch.Start();

            Stream originalBody = context.Response.Body;

            context.Response.OnStarting(state =>
            {
                watch.Stop();

                var httpContext = (HttpContext)state;

                httpContext.Response.Body.Position = 0;

                string bodyText = new StreamReader(httpContext.Response.Body).ReadToEnd();

                if (!string.IsNullOrWhiteSpace(bodyText))
                {
                    JObject jo = JObject.Parse(bodyText);
                    jo.Add("tempoLevado2", watch.Elapsed.ToString());
                    bodyText = jo.ToString();

                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes("{\r\n  \"tempoLevado2\": \"00:00:06.8132217\"\r\n}" ?? "")))
                    {
                        httpContext.Response.Body = new MemoryStream();
                        ms.CopyTo(httpContext.Response.Body);
                    }
                }

                return Task.CompletedTask;
            }, context);

            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;

                await _next(context);

                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);

                context.Response.Body = originalBody;
                
                //string bodyText = new StreamReader(memStream).ReadToEnd();

                //await context.Response.Body.WriteAsync(memStream.ToArray(), 0, (int)memStream.Length);
            }
        }
    }

    /// <summary>
    /// Response extension
    /// </summary>
    public static class ResponseTimeMiddlewareExtensions
    {
        /// <summary>
        /// Builder for response http context
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}
