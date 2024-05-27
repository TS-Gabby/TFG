using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDBTFG.Model
{
    [Serializable]
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public required string Password { get; set; }
        public float Dinero { get; set; } = 0f;

        public int? RolId { get; set; }


    }
}
