using System.ComponentModel.DataAnnotations;

namespace ApiRest.Models
{
    public class Rol
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public List<Usuario> Usuarios { get; set; } = [];
    }
}
