using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace demo.ethm.WebApi.Authorization
{
    /// <summary>
    /// Types of authentications
    /// </summary>
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Default auth
        /// </summary>
        public static string DefaultSchemeName = "";

        /// <summary>
        /// Bearer, the auth we implement
        /// </summary>
        public static string BearerSchemeName = "bearer";

        /// <summary>
        /// Basic
        /// </summary>
        public static string BasicSchemeName = "basic";
    }
}