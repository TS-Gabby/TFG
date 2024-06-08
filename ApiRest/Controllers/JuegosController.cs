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
    public class JuegosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public JuegosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Juegos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Juego>>> GetJuego()
        {
            return await _context.Juego.ToListAsync();
        }

        // GET: api/Juegos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Juego>> GetJuego(int id)
        {
            var juego = await _context.Juego.FindAsync(id);

            if (juego == null)
            {
                return NotFound();
            }

            return juego;
        }

        // PUT: api/Juegos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJuego(int id, Juego juego)
        {
            if (id != juego.Id)
            {
                return BadRequest();
            }

            _context.Entry(juego).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuegoExists(id))
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

        // POST: api/Juegos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Juego>> PostJuego(Juego juego)
        {
            _context.Juego.Add(juego);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJuego", new { id = juego.Id }, juego);
        }

        // DELETE: api/Juegos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuego(int id)
        {
            var juego = await _context.Juego.FindAsync(id);
            if (juego == null)
            {
                return NotFound();
            }

            _context.Juego.Remove(juego);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuegoExists(int id)
        {
            return _context.Juego.Any(e => e.Id == id);
        }

    }
}
