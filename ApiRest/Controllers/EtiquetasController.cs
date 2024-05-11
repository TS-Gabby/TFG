using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRest.Context;
using ApiRest.Models;

namespace ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtiquetasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EtiquetasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Etiquetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etiqueta>>> GetEtiqueta()
        {
            return await _context.Etiqueta.ToListAsync();
        }

        // GET: api/Etiquetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Etiqueta>> GetEtiqueta(int id)
        {
            var etiqueta = await _context.Etiqueta.FindAsync(id);

            if (etiqueta == null)
            {
                return NotFound();
            }

            return etiqueta;
        }

        // PUT: api/Etiquetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtiqueta(int id, Etiqueta etiqueta)
        {
            if (id != etiqueta.Id)
            {
                return BadRequest();
            }

            _context.Entry(etiqueta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtiquetaExists(id))
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

        // POST: api/Etiquetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Etiqueta>> PostEtiqueta(Etiqueta etiqueta)
        {
            _context.Etiqueta.Add(etiqueta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtiqueta", new { id = etiqueta.Id }, etiqueta);
        }

        // DELETE: api/Etiquetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtiqueta(int id)
        {
            var etiqueta = await _context.Etiqueta.FindAsync(id);
            if (etiqueta == null)
            {
                return NotFound();
            }

            _context.Etiqueta.Remove(etiqueta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtiquetaExists(int id)
        {
            return _context.Etiqueta.Any(e => e.Id == id);
        }
    }
}
