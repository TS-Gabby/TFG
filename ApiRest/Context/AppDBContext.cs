using ApiRest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Etiqueta> Etiqueta { get; set; }
        public DbSet<Juego> Juego { get; set; }
        public DbSet<Rol> Rol { get; set; }

        public DbSet<JuegoXEtiqueta> JuegoXEtiqueta { get; set; }
        public DbSet<UsuarioXJuego> UsuarioXJuego { get; set; }
    }
}
