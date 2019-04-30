using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace demo.ethm.WebApi.Authorization
{
    /// <summary>
    /// Extension for validate claims
    /// </summary>
    public static class CustomAuthExtensions
    {
        /// <summary>
        /// Add authentication
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<CustomAuthOptions> configureOptions)
        {
            return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>(CustomAuthOptions.DefaultSchemeName, "Auth", configureOptions);
        }
    }
}