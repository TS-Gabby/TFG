using Microsoft.EntityFrameworkCore;

namespace ApiRest.Models
{
    [PrimaryKey(nameof(Id_Usuario), nameof(Id_Juego))]
    public class UsuarioXJuego
    {
        public required int Id_Usuario { get; set; }
        public required int Id_Juego { get; set; }

        //Propiedades de navegación
        public Usuario Usuario { get; set; } = null!;
        public Juego Juego { get; set; } = null!;
    }
}
