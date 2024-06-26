using MarketPlace.DAL.DataContext;
using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbMarketPlaceContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DbMarketPlaceContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        public Usuario Authenticate(UsuarioLoginDTO usuario)
        {
            try
            {
                var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario.ToLower() == usuario.NombreUsuario.ToLower()
                                                        && u.Contraseña.ToLower() == usuario.Contraseña.ToLower());
                return  user;

            }catch (Exception ex)
            {
                return null;
            }

        }

        public string GenerateToken(Usuario usuario, string rol)
        {
            //Credenciales
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, rol),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString())

            };

            //Token
            var token = new JwtSecurityToken(
                 _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

        public Rol GetRole(Usuario usuario)
        {
            var query = from u in _context.Usuarios
                        join ru in _context.RolUsuarios on u.IdUsuario equals ru.UsuarioIdUsuario
                        join r in _context.Rols on ru.RolIdRol equals r.IdRol
                        where u.NombreUsuario == usuario.NombreUsuario && u.Contraseña == usuario.Contraseña 
                        select new
                        {
                            //u.IdUsuario,
                            rol = new Rol { IdRol = r.IdRol, Nombre = r.Nombre },
                        };

            var userRol = query.FirstOrDefault().rol;

            

            return userRol;
        }

        public Usuario GetUser(String nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }
    }
}
