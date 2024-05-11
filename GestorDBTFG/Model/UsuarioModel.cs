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
        public UsuarioModel(int id, string name, float money, int? idRol)
        {
            Id = id;
            Name = name;
            Money = money;
            IdRol = idRol;
        }
        public UsuarioModel(int id, string name, float money)
        {
            Id = id;
            Name = name;
            Money = money;
            IdRol = null;
        }
        public UsuarioModel(int id, string name)
        {
            Id = id;
            Name = name;
            Money = 0f;
            IdRol = null;
        }
        public UsuarioModel()
        {
            Id = 0;
            Name = "";
            Money = 0f;
            IdRol = null;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public float Money { get; set; }
        public int? IdRol { get; set; }


    }
}
