using demo.ethm.Aplicacao.EntityExtensions;
using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Idiomas suportados pelo sistemas
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class IdiomasController : ControllerBase
    {
        private readonly MainContext _context;

        /// <summary>
        /// Construtor do idioma
        /// </summary>
        /// <param name="context"></param>
        public IdiomasController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter uma lista de idiomas com descrição e identificador
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<IdiomaDTO> GetIdioma()
        {
            return _context.Idioma
                .Where(x => !x.DataDeDesativacao.HasValue)
                .ToList()
                .Traduzir();
        }
    }
}
