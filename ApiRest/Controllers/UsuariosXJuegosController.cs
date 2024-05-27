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
    public class UsuariosXJuegosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UsuariosXJuegosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/UsuariosXJuegos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioXJuego>>> GetUsuarioXJuego()
        {
            return await _context.UsuarioXJuego.ToListAsync();
        }

        // GET: api/UsuariosXJuegos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioXJuego>> GetUsuarioXJuego(int id)
        {
            var usuarioXJuego = await _context.UsuarioXJuego.FindAsync(id);

            if (usuarioXJuego == null)
            {
                return NotFound();
            }

            return usuarioXJuego;
        }

        // PUT: api/UsuariosXJuegos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioXJuego(int id, UsuarioXJuego usuarioXJuego)
        {
            if (id != usuarioXJuego.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarioXJuego).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioXJuegoExists(id))
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

        // POST: api/UsuariosXJuegos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioXJuego>> PostUsuarioXJuego(UsuarioXJuego usuarioXJuego)
        {
            _context.UsuarioXJuego.Add(usuarioXJuego);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioXJuegoExists(usuarioXJuego.IdUsuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuarioXJuego", new { id = usuarioXJuego.IdUsuario }, usuarioXJuego);
        }

        // DELETE: api/UsuariosXJuegos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioXJuego(int id)
        {
            var usuarioXJuego = await _context.UsuarioXJuego.FindAsync(id);
            if (usuarioXJuego == null)
            {
                return NotFound();
            }

            _context.UsuarioXJuego.Remove(usuarioXJuego);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioXJuegoExists(int id)
        {
            return _context.UsuarioXJuego.Any(e => e.IdUsuario == id);
        }
    }
}
