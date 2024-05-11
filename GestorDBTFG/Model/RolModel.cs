using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDBTFG.Model
{
    [Serializable] 
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


        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
