using MarketPlace.DAL.Repositories;
using MarketPlace.Models.DTOs;
using MarketPlace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketPlace.BLL.Validations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace MarketPlace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _repo;
        private readonly IValidations _validations;

        public CategoriaController(ICategoriaRepository repo, IValidations validations)
        {
            _repo = repo;
            _validations = validations;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Get()
        {
            var categorias = _repo.GetAll();

            if (categorias.Count() <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No hay categorias",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de categorias",
                result = categorias
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult GetById(int id)
        {
            var categoria = _repo.FindById(id);

            if (categoria == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Categoria no encontrada",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Categoria",
                result = categoria
            });
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Add([FromBody] CategoriaDTO categoria)
        {
            // Verifica que se ingrese un valor para el nombre
            if (categoria.Nombre.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Agregue un nombre para la categoría",
                    result = ""
                });
            }

            // Verifica si existe una categoria con el mismo nombre
            bool categoriaExist = _validations.CategoriaExistByName(categoria.Nombre);

            if (categoriaExist)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No puede agregar dos categorías iguales",
                    result = ""
                });
            }

            // Llama al método del respositorio para agregar la categoria
            Categoria category = new Categoria();
            category.Nombre = categoria.Nombre;
            category.Estado = 1;

            bool isAdd = _repo.Insert(category);

            if (!isAdd)
            {
                // Mensaje si no se agregó la categoria
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ocurrió un error inesperado y no se pudo agregar la categoría",
                    result = ""
                });
            }

            // Mensaje de confirmación
            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Categoría agregada con éxito",
                result = ""
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Update([FromBody] CategoriaDTO categoria, int id)
        {
            // Verifica que se ingrese un valor para el nombre
            if (categoria.Nombre.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Agregue un nombre para la categoría",
                    result = ""
                });
            }

            // Verifica si existe una categoria con el mismo nombre
            bool categoriaExist = _validations.CategoriaExistByName(categoria.Nombre);

            if (categoriaExist)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No puede agregar dos categorías iguales",
                    result = ""
                });
            }

            // Llama al método del respositorio para actualizar la categoria
            Categoria category = new Categoria();
            category.Nombre = categoria.Nombre;
            category.Estado = 1;

            bool isUpdated = _repo.Update(category, id);

            if (!isUpdated)
            {
                // Mensaje si no se actualizó la categoria
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Categoría no encontrada",
                    result = ""
                });
            }

            // Mensaje de confirmación
            return StatusCode(StatusCodes.Status202Accepted, new
            {
                message = "Categoría actualizada con éxito",
                result = ""
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _repo.DeleteById(id);

            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Categoría no encontrada",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status202Accepted, new
            {
                message = "Categoría eliminada con éxito",
                result = ""
            });
        }
    }
}
