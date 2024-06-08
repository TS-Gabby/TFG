using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model
{
    [Serializable] 
    public class JuegoModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Compania { get; set; }

        public bool Accesible_PC { get; set; }
        public bool Accesible_M { get; set; }

        public float Descuento { get; set; }
        public int Precio { get; set; }
    }
}
