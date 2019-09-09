using demo.ethm.Aplicacao.EntityExtensions;
using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
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
    /// Classe com definições país suportada pela aplicação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly MainContext _context;

        /// <summary>
        /// Construtor do controle de país
        /// </summary>
        /// <param name="context"></param>
        public PaisController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter uma lista de todos os países suportados pelo sistema com tradução pelo idioma do usuário
        /// </summary>
        /// <param name="idIdioma"></param>
        /// <returns></returns>
        [HttpGet("{idIdioma}")]
        public IEnumerable<PaisDTO> GetPais(int idIdioma)
        {
            return _context.Pais.Include(x => x.Descricoes).ToList().Traduzir(idIdioma);
        }
    }
}
