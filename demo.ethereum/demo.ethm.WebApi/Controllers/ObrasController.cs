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
    /// Manter Obras literárias
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : DemoControllerBase
    {
        private readonly MainContext _context;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="context"></param>
        public ObrasController(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of Obras of the login user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obra>>> GetObra()
        {
            return await _context.Obra.ToListAsync();
        }

        /// <summary>
        /// Get data of Obra of the login user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Obra>> GetObra(int id)
        {
            var obra = await _context.Obra.FindAsync(id);

            if (obra == null)
            {
                return NotFound();
            }

            return obra;
        }

        /// <summary>
        /// Update Obra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obra"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObra(int id, Obra obra)
        {
            if (id != obra.Id)
            {
                return BadRequest();
            }

            _context.Entry(obra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObraExists(id))
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
        /// Create new Obra with status "Pendente"
        /// </summary>
        /// <param name="obra"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Obra>> PostObra(Obra obra)
        {
            _context.Obra.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObra", new { id = obra.Id }, obra);
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Obra>> DeleteObra(int id)
        {
            var obra = await _context.Obra.FindAsync(id);
            if (obra == null)
            {
                return NotFound();
            }

            _context.Obra.Remove(obra);
            await _context.SaveChangesAsync();

            return obra;
        }

        private bool ObraExists(int id)
        {
            return _context.Obra.Any(e => e.Id == id);
        }
    }
}
