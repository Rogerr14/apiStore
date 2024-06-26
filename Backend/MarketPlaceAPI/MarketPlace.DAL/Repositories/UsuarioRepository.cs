using MarketPlace.DAL.DataContext;
using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DAL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbMarketPlaceContext _context;

        public UsuarioRepository(DbMarketPlaceContext context)
        {
            _context = context;
        }

        public bool Registrer(UsuarioDTO usuario)
        {
            try
            {
                Usuario user = new Usuario();

                user.Cedula = usuario.Cedula;
                user.NombreUsuario = usuario.NombreUsuario;
                user.CorreoElectronico = usuario.CorreoElectronico;
                user.Contraseña = usuario.Contraseña;
                user.Estado = 1;

                _context.Usuarios.Add(user);
                _context.SaveChanges();

                RolUsuario rUsuario = new RolUsuario();
                rUsuario.UsuarioIdUsuario = user.IdUsuario;
                rUsuario.RolIdRol = 2;

                _context.RolUsuarios.Add(rUsuario);
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
