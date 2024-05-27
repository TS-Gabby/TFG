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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.RolFK)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);


            modelBuilder.Entity<UsuarioXJuego>()
                .HasKey(uj => new { uj.IdUsuario, uj.IdJuego });

            modelBuilder.Entity<UsuarioXJuego>()
                .HasOne(uj => uj.UsuarioFK)
                .WithMany(u => u.UsuarioXJuego)
                .HasForeignKey(uj => uj.IdUsuario);

            modelBuilder.Entity<UsuarioXJuego>()
                .HasOne(uj => uj.JuegoFK)
                .WithMany(j => j.UsuarioXJuego)
                .HasForeignKey(uj => uj.IdJuego);


            modelBuilder.Entity<JuegoXEtiqueta>()
                .HasKey(je => new { je.IdJuego, je.IdEtiqueta });

            modelBuilder.Entity<JuegoXEtiqueta>()
                .HasOne(ej => ej.JuegoFK)
                .WithMany(j => j.JuegoXEtiquetas)
                .HasForeignKey(je => je.IdJuego);

            modelBuilder.Entity<JuegoXEtiqueta>()
                .HasOne(je => je.EtiquetaFK)
                .WithMany(e => e.JuegoXEtiquetas)
                .HasForeignKey(je => je.IdEtiqueta);
        }
    }
}
