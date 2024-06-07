using Microsoft.EntityFrameworkCore;

namespace ApiRest.Models
{
    public class UsuarioXJuego
    {
        public required int IdUsuario { get; set; }
        public required int IdJuego { get; set; }

        //Propiedades de navegación
        public Usuario? UsuarioFK { get; set; } = null!;
        public Juego? JuegoFK { get; set; } = null!;
    }
}
