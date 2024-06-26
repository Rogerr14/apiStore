using MarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Validations
{
    public interface IValidations
    {
        public bool CategoriaExist(int? id);
        public bool CategoriaExistByName(string nombre);
        public bool UsuarioExistByCedula(string cedula);
        public bool UsuarioExistByUserName(string nombre);
        public bool ProductoExistByName(string nombre);
        public bool ProductosExistUpdateByName(string nombre);
    }
}
