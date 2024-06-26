using MarketPlace.DAL.DataContext;
using MarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DAL.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DbMarketPlaceContext _context;

        public CategoriaRepository(DbMarketPlaceContext context)
        {
            _context = context;
        }

        public List<Categoria> GetAll()
        {
            return _context.Categoria.Where(c => c.Estado == 1).ToList();
        }
        
        public Categoria FindById(int id)
        {
            return _context.Categoria.FirstOrDefault(c => c.IdCategoria == id && c.Estado == 1);
        }

        public bool Insert(Categoria model)
        {
            try
            {
                _context.Categoria.Add(model);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Models.Categoria model, int id)
        {
            try
            {
                var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id && c.Estado == 1);

                if (categoria == null)
                {
                    return false;
                }

                categoria.Nombre = model.Nombre;
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
                var categoria = _context.Categoria.FirstOrDefault(c => c.IdCategoria == id && c.Estado == 1);

                if (categoria == null)
                {
                    return false;
                }

                categoria.Estado = 0;
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
