using System.ComponentModel.DataAnnotations;

namespace ApiRest.Models
{
    public class Etiqueta
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public List<JuegoXEtiqueta> JuegoXEtiquetas { get; } = [];
    }
}
