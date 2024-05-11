using Microsoft.EntityFrameworkCore;

namespace ApiRest.Models
{
    [PrimaryKey(nameof(Id_Juego), nameof(Id_Etiqueta))]
    public class JuegoXEtiqueta
    {
        public int Id_Juego { get; set; }
        public int Id_Etiqueta { get; set; }

        //Propiedades de navegación
        public Juego Juego { get; set; } = null!;
        public Etiqueta Etiqueta { get; set; } = null!;

    }
}
