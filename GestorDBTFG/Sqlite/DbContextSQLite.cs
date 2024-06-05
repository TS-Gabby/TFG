using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorDBTFG.Sqlite.Models;
using GestorDBTFG.Model;
using SQLite;

namespace GestorDBTFG.Sqlite
{
    public class DbContextSQLite
    {
        readonly SQLiteAsyncConnection _database;

        public DbContextSQLite(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UsuarioActual>().Wait();
        }

        public Task<List<UsuarioActual>> GetUsuariosAsync()
        {
            return _database.Table<UsuarioActual>().ToListAsync();
        }

        public Task<int> SaveUsuarioAsync(UsuarioActual usuario)
        {
            if (usuario.Id != 0)
            {
                return _database.UpdateAsync(usuario);
            }
            else
            {
                return _database.InsertAsync(usuario);
            }
        }

        public Task<int> DeleteUsuarioAsync(UsuarioActual usuario)
        {
            return _database.DeleteAsync(usuario);
        }
    }
}
