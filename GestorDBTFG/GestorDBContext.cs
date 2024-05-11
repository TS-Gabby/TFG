using GestorDBTFG.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDBTFG
{
    internal class GestorDBContext : DbContext
    {
        public GestorDBContext()
        {
        }
        public GestorDBContext(DbContextOptions<GestorDBContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<UsuarioModel> USUARIO => Set<UsuarioModel>();
        public DbSet<RolModel> ROL => Set<RolModel>();

        #endregion
    }
}
