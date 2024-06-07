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
    public class JuegoXEtiquetasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public JuegoXEtiquetasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/JuegoXEtiquetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JuegoXEtiqueta>>> GetJuegoXEtiqueta()
        {
            return await _context.JuegoXEtiqueta.ToListAsync();
        }

        // GET: api/JuegoXEtiquetas/5
        [HttpGet("{idJuego}/{idEtiqueta}")]
        public async Task<ActionResult<JuegoXEtiqueta>> GetJuegoXEtiqueta(int idJuego, int idEtiqueta)
        {
            var juegoXEtiqueta = await _context.JuegoXEtiqueta.FindAsync(idJuego, idEtiqueta);

            if (juegoXEtiqueta == null)
            {
                return NotFound();
            }

            return juegoXEtiqueta;
        }

        // PUT: api/JuegoXEtiquetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idJuego}/{idEtiqueta}")]
        public async Task<IActionResult> PutJuegoXEtiqueta(int idJuego, int idEtiqueta, JuegoXEtiqueta juegoXEtiqueta)
        {
            if (idJuego != juegoXEtiqueta.IdJuego && idEtiqueta != juegoXEtiqueta.IdEtiqueta)
            {
                return BadRequest();
            }

            _context.Entry(juegoXEtiqueta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuegoXEtiquetaExists(idJuego, idEtiqueta))
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

        // POST: api/JuegoXEtiquetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JuegoXEtiqueta>> PostJuegoXEtiqueta(JuegoXEtiqueta juegoXEtiqueta)
        {
            _context.JuegoXEtiqueta.Add(juegoXEtiqueta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JuegoXEtiquetaExists(juegoXEtiqueta.IdJuego, juegoXEtiqueta.IdEtiqueta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJuegoXEtiqueta", new { id = juegoXEtiqueta.IdJuego }, juegoXEtiqueta);
        }

        // DELETE: api/JuegoXEtiquetas/5
        [HttpDelete("{idJuego}/{idEtiqueta}")]
        public async Task<IActionResult> DeleteJuegoXEtiqueta(int idJuego, int idEtiqueta)
        {
            var juegoXEtiqueta = await _context.JuegoXEtiqueta.FindAsync(idJuego, idEtiqueta);
            if (juegoXEtiqueta == null)
            {
                return NotFound();
            }

            _context.JuegoXEtiqueta.Remove(juegoXEtiqueta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuegoXEtiquetaExists(int idJuego, int idEtiqueta)
        {
            return _context.JuegoXEtiqueta.Any(e => e.IdJuego == idJuego && e.IdEtiqueta == idEtiqueta);
        }
    }
}
