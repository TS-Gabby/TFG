using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }
        public required string Password { get; set; }
        public float Dinero { get; set; } = 0f;

        public int RolId{ get; set; }
        public Rol? RolFK { get; set; }

        public List<UsuarioXJuego> UsuarioXJuego { get; } = [];
    }
}
