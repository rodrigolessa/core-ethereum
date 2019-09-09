using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Infraestrutura.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Lista de enumeradores que definem situações e tipos de registros na aplicação
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EnumeradoresController : ControllerBase
    {
        /// <summary>
        /// Definição dos Tipos para o cadastro de usuário
        /// </summary>
        /// <param name="IdIdioma"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("TiposDeUsuario")]
        public string TiposDeUsuario([FromHeader] int IdIdioma)
        {
            return EnumMethods.ObterTiposDeUsuario(IdIdioma);
        }
    }
}
