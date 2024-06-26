using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Services
{
    public interface IAuthService
    {
        public Usuario Authenticate(UsuarioLoginDTO usuario);
        public Rol GetRole(Usuario usuario);
        public string GenerateToken(Usuario usuario, string rol);

        public Usuario GetUser(string userName);

        
    }
}
