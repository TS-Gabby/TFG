using System.ComponentModel.DataAnnotations;

namespace ApiRest.Models
{
    public class Juego
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }
        public string? Compania { get; set; }

        public bool Accesible_PC { get; set; } = false;
        public bool Accesible_M { get; set;} = false;

        public float Descuento { get; set; } = 0;
        public required float Precio { get; set; }

        public List<UsuarioXJuego> UsuarioXJuego { get; } = [];
        public List<JuegoXEtiqueta> JuegoXEtiquetas { get; } = [];
    }
}
