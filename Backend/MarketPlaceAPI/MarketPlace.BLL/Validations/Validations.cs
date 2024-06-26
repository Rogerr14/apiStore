using MarketPlace.DAL.DataContext;
using MarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Validations
{
    public class Validations : IValidations
    {
        private readonly DbMarketPlaceContext _context;

        public Validations(DbMarketPlaceContext context)
        {
            _context = context;
        }

        public bool CategoriaExist(int? id)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id);

            if (categoria == null) return false;

            return true;
        }

        public bool CategoriaExistByName(string nombre)
        {
            var categoria = _context.Categoria.FirstOrDefault(c => c.Nombre.Equals(nombre));

            if (categoria == null) return false;

            return true;
        }

        public bool ProductoExistByName(string nombre)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Nombre.Equals(nombre));

            if (producto == null) return false;

            return true;
        }

        public bool UsuarioExistByCedula(string cedula)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Cedula.Equals(cedula));

            if (usuario == null) return false;

            return true;
        }

        public bool UsuarioExistByUserName(string nombre)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario.Equals(nombre));

            if (usuario == null) return false;

            return true;
        }

        public bool ProductosExistUpdateByName(string nombre)
        {
            var usuarios = _context.Usuarios.Where(u => u.NombreUsuario.Equals(nombre)).ToList();

            if (usuarios.Count > 1) return true;

            return false;
        }
    }
}
