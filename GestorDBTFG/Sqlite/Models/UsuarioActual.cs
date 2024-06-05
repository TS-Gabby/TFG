using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace GestorDBTFG.Sqlite.Models
{
    public class UsuarioActual
    {
        public UsuarioActual()
        {
            
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int Code { get; set; }

        public string Nombre { get; set; } = null!;
        public string Password { get; set; }
        public float Dinero { get; set; } = 0f;

        public int? RolId { get; set; }
    }
}
