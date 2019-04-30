using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
using Microsoft.AspNetCore.Authorization;

namespace demo.ethm.WebApi.Controllers
{
    /// <summary>
    /// Lista de Representantes
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentantesController : DemoControllerBase
    {
        private readonly MainContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="context"></param>
        public RepresentantesController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// List of
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Representante>>> GetRepresentante()
        {
            return await _context.Representante.ToListAsync();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Representante>> GetRepresentante(int id)
        {
            var representante = await _context.Representante.FindAsync(id);

            if (representante == null)
            {
                return NotFound();
            }

            return representante;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="representante"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepresentante(int id, Representante representante)
        {
            if (id != representante.Id)
            {
                return BadRequest();
            }

            _context.Entry(representante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepresentanteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Create new
        /// </summary>
        /// <param name="representante"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Representante>> PostRepresentante(Representante representante)
        {
            _context.Representante.Add(representante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepresentante", new { id = representante.Id }, representante);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Representante>> DeleteRepresentante(int id)
        {
            var representante = await _context.Representante.FindAsync(id);
            if (representante == null)
            {
                return NotFound();
            }

            _context.Representante.Remove(representante);
            await _context.SaveChangesAsync();

            return representante;
        }

        private bool RepresentanteExists(int id)
        {
            return _context.Representante.Any(e => e.Id == id);
        }
    }
}
