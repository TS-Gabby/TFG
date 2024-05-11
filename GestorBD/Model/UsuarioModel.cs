using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBD.Model
{
    internal class UsuarioModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public float Money { get; set; }
        public int? IdRol { get; set; }
    }
}
