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
    public class JuegosXEtiquetasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public JuegosXEtiquetasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/JuegosXEtiquetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JuegoXEtiqueta>>> GetJuegoXEtiqueta()
        {
            return await _context.JuegoXEtiqueta.ToListAsync();
        }

        // GET: api/JuegosXEtiquetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JuegoXEtiqueta>> GetJuegoXEtiqueta(int id)
        {
            var juegoXEtiqueta = await _context.JuegoXEtiqueta.FindAsync(id);

            if (juegoXEtiqueta == null)
            {
                return NotFound();
            }

            return juegoXEtiqueta;
        }

        // PUT: api/JuegosXEtiquetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJuegoXEtiqueta(int id, JuegoXEtiqueta juegoXEtiqueta)
        {
            if (id != juegoXEtiqueta.Id_Juego)
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
                if (!JuegoXEtiquetaExists(id))
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

        // POST: api/JuegosXEtiquetas
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
                if (JuegoXEtiquetaExists(juegoXEtiqueta.Id_Juego))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJuegoXEtiqueta", new { id = juegoXEtiqueta.Id_Juego }, juegoXEtiqueta);
        }

        // DELETE: api/JuegosXEtiquetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuegoXEtiqueta(int id)
        {
            var juegoXEtiqueta = await _context.JuegoXEtiqueta.FindAsync(id);
            if (juegoXEtiqueta == null)
            {
                return NotFound();
            }

            _context.JuegoXEtiqueta.Remove(juegoXEtiqueta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuegoXEtiquetaExists(int id)
        {
            return _context.JuegoXEtiqueta.Any(e => e.Id_Juego == id);
        }
    }
}
