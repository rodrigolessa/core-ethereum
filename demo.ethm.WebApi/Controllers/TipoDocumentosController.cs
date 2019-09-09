using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
using demo.ethm.Aplicacao.EntityExtensions;
using demo.ethm.Aplicacao.Models;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Definições para os tipos de documentos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentosController : ControllerBase
    {
        private readonly MainContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="context"></param>
        public TipoDocumentosController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get types of documents
        /// </summary>
        /// <param name="idIdioma"></param>
        /// <returns></returns>
        [HttpGet("idIdioma")]
        public IEnumerable<TipoDocumentoDTO> GetTipoDocumento(int idIdioma)
        {
            return _context.TipoDocumento
                .Include(x => x.Descricoes)
                .ToList()
                .Traduzir(idIdioma);
        }
    }
}
