using MarketPlace.BLL.Services;
using MarketPlace.BLL.Validations;
using MarketPlace.DAL.Repositories;
using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace MarketPlace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _repo;
        private readonly IValidations _validations;

        public AuthController(IAuthService auth, IUsuarioRepository repo, IValidations validations)
        {
            _authService = auth;
            _repo = repo;
            _validations = validations;
        }

        [HttpPost("Login")]
        public IActionResult login([FromBody] UsuarioLoginDTO usuario)
        {
            var user = _authService.Authenticate(usuario);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    messsage = "Usuario no encontrado, credenciales incorrectas",
                    error = true,
                    data = user
                });
            }
            else
            {

            Rol rol = _authService.GetRole(user);

            string token = _authService.GenerateToken(user, rol.Nombre);

            Usuario userData = _authService.GetUser(user.NombreUsuario);

                var data = new
                {
                    Token = token,
                    nombre = userData.NombreUsuario,
                    correo = userData.CorreoElectronico,
                    rol = rol.IdRol
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = "",
                    error = false,
                    data = data
                });
            }

        }

        [HttpPost("Registrer")]
        public IActionResult Registrer([FromBody] UsuarioDTO usuario)
        {
            // Verifica si el nombre de usuario es nulo o está vacio o si tiene mas o menos de 10 digitos
            if (usuario.Cedula.IsNullOrEmpty() || usuario.Cedula.Length != 10)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número de cedula válido"
                });
            }

            // Verifica se la cedula contiene letras
            if(!Regex.IsMatch(usuario.Cedula, @"^\d+$"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El número de cedula solo debe contener números.",
                    error = true 
                });
            }

            // Verifica si la contraseña no contiene espacios
            if (usuario.Cedula.Contains(" "))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La cedula no debe contener espacios.",
                    //data = null
                });
            }

            // Verifica si el nombre de usuario es nulo o está vacio
            if (usuario.NombreUsuario.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un nombre de usuario"
                });
            }

            // Verifica si el nombre de usuario contiene mas de 20 caracteres
            if (usuario.NombreUsuario.Length > 20)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario no puede contener más de 20 caracteres"
                });
            }

            // Verifica si el nombre de usuario no contiene signos
            if (Regex.IsMatch(usuario.NombreUsuario, @"[^\w\d]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario no puede contener signos"
                });
            }

            // Verifica si el nombre de usuario contiene al menos un número
            if (!Regex.IsMatch(usuario.NombreUsuario, @"\d"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario debe contener al menos un número.",
                    result = ""
                });
            }

            // Verifica si el nombre de usuario contiene al menos una letra mayúscula
            if (!Regex.IsMatch(usuario.NombreUsuario, @"[A-Z]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario debe contener al menos una letra mayúscula.",
                    result = ""
                });
            }

            // Verifica si existe un usuario con el mismo nombre
            bool usuarioExistByName = _validations.UsuarioExistByCedula(usuario.Cedula);

            if (usuarioExistByName)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No puede agregar usuarios con cedula duplicada",
                    error = true
                });
            }

            // Verifica que se ingrese un valor para el correo electronico
            if (usuario.CorreoElectronico.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un correo electronico"
                });
            }

            // Verifica que se ingrese un valor para el correo electronico
            if (usuario.Contraseña.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una contraseña"
                });
            }

            // Verifica si la contraseña tiene al menos 10 caracteres
            if (usuario.Contraseña.Length > 10)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña no puede tener al menos 10 caracteres.",
                    result = ""
                });
            }

            // Verifica si la contraseña contiene al menos una letra mayúscula
            if (!Regex.IsMatch(usuario.Contraseña, @"[A-Z]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña debe contener al menos una letra mayúscula.",
                    result = ""
                });
            }

            // Verifica si la contraseña no contiene espacios
            if (usuario.Contraseña.Contains(" "))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña no debe contener espacios.",
                    result = ""
                });
            }

            // Verifica si la contraseña contiene al menos un signo
            if (!Regex.IsMatch(usuario.Contraseña, @"[\W]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña debe contener al menos un signo.",
                    result = ""
                });
            }

            // Verifica el formato del correo electronico
            //if (!Regex.IsMatch(usuario.CorreoElectronico, @"^@$"))
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, new
            //    {
            //        message = "El correo electrónico no es válido.",
            //        result = ""
            //    });
            //}

            // Verifica si existe un usuario con la misma cedula
            bool usuarioExist = _validations.UsuarioExistByCedula(usuario.Cedula);

            if (usuarioExist)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No puede agregar usuarios con la misma cedula"
                });
            }

            // Llama al método para registrar el usuario y a su vez el rol cliente
            bool isRegistred = _repo.Registrer(usuario);

            if (!isRegistred)
            {
                // Mensaje si no se pudo agregar correctamente el usuario o su rol
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo registrar el usuario"
                });
            }

            // Mensaje de confirmación
            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Usuario registrado con éxito"
            });
        }
    }
}
