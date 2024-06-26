using MarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DAL.Repositories
{
    public interface ICategoriaRepository
    {
        public List<Categoria> GetAll();
        public Categoria FindById(int id);
        public bool Insert(Categoria model);
        public bool Update(Categoria model, int id);
        public bool DeleteById(int id);
    }
}
