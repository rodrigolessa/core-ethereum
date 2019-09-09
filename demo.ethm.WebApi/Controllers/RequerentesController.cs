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
    /// Lista de requerentes
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequerentesController : DemoControllerBase
    {
        private readonly MainContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="context"></param>
        public RequerentesController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requerente>>> GetRequerente()
        {
            return await _context.Requerente.ToListAsync();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Requerente>> GetRequerente(int id)
        {
            var requerente = await _context.Requerente.FindAsync(id);

            if (requerente == null)
            {
                return NotFound();
            }

            return requerente;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requerente"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequerente(int id, Requerente requerente)
        {
            if (id != requerente.Id)
            {
                return BadRequest();
            }

            _context.Entry(requerente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequerenteExists(id))
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
        /// <param name="requerente"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Requerente>> PostRequerente(Requerente requerente)
        {
            _context.Requerente.Add(requerente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequerente", new { id = requerente.Id }, requerente);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Requerente>> DeleteRequerente(int id)
        {
            var requerente = await _context.Requerente.FindAsync(id);
            if (requerente == null)
            {
                return NotFound();
            }

            _context.Requerente.Remove(requerente);
            await _context.SaveChangesAsync();

            return requerente;
        }

        private bool RequerenteExists(int id)
        {
            return _context.Requerente.Any(e => e.Id == id);
        }
    }
}
