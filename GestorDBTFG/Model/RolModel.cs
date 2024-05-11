using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDBTFG.Model
{
    public class RolModel
    {
        public RolModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public RolModel(int id)
        {
            Id = id;
            Name = "";
        }
        public RolModel()
        {
            Id = 0;
            Name = "";
        }


        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
