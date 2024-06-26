using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DAL.Repositories
{
    public interface IUsuarioRepository
    {
        public bool Registrer(UsuarioDTO usuario);
    }
}
