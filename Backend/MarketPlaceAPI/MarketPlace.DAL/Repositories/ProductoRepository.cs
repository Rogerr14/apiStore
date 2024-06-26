using MarketPlace.DAL.DataContext;
using MarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DAL.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DbMarketPlaceContext _context;

        public ProductoRepository(DbMarketPlaceContext context)
        {
            _context = context;
        }

        public List<Producto> GetAll()
        {
            return _context.Productos.Where(p => p.Estado == 1).ToList();
        }

        public Producto FindById(int id)
        {
            return _context.Productos.FirstOrDefault(p => p.IdProducto == id && p.Estado == 1);
        }

        public bool Insert(Producto model, int? id)
        {
            try
            {
                _context.Productos.Add(model);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Producto model, int id, int? IdCategoria)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id && p.Estado == 1);

                if (producto == null)
                {
                    return false;
                }

                producto.Nombre = model.Nombre;
                producto.Stock = model.Stock;
                producto.PrecioUnitario = model.PrecioUnitario;
                producto.UrlImagen = model.UrlImagen;
                producto.Descripcion = model.Descripcion;

                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteById(int id)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id && p.Estado == 1);

                if (producto == null)
                {
                    return false;
                }

                producto.Estado = 0;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
