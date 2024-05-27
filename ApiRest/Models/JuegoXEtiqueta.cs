using Microsoft.EntityFrameworkCore;

namespace ApiRest.Models
{
    public class JuegoXEtiqueta
    {
        public int IdJuego { get; set; }
        public int IdEtiqueta { get; set; }

        //Propiedades de navegación
        public Juego JuegoFK { get; set; } = null!;
        public Etiqueta EtiquetaFK { get; set; } = null!;

    }
}
