using MarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DAL.Repositories
{
    public interface IProductoRepository
    {
        public List<Producto> GetAll();
        public Producto FindById(int id);
        public bool Insert(Producto model, int? id);
        public bool Update(Producto model, int id, int? idCategoria);
        public bool DeleteById(int id);
    }
}
