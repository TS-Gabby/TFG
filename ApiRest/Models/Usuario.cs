using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }
        public float Dinero { get; set; } = 0f;

        public int? IdRol { get; set; }
        public Rol? Rol { get; set; }

        public List<UsuarioXJuego> UsuarioXJuego { get; } = [];
    }
}
