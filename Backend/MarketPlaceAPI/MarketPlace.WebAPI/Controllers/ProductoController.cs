using MarketPlace.BLL.Validations;
using MarketPlace.DAL.Repositories;
using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace MarketPlace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _repo;
        private readonly IValidations _validations;

        public ProductoController(IProductoRepository repo, IValidations validations)
        {
            _repo = repo;
            _validations = validations;
        }

        [Authorize(Roles = "Cliente, Administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            var productos = _repo.GetAll();

            if (productos.Count() <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No hay productos",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de productos",
                result = productos
            });
        }

        [Authorize(Roles = "Cliente, Administrador")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producto = _repo.FindById(id);

            if (producto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Producto no encontrado",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Producto",
                result = producto
            });
        }

        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPost]
        public IActionResult Add([FromBody] ProductoDTO producto)
        {
            // Verifica que se ingrese un valor para el nombre
            if (producto.Nombre.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un nombre para el producto",
                    result = ""
                });
            }

            // Verifica si el nombre del producto tiene más de 20 caracteres
            if (producto.Nombre.Length > 20)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre del producto no puede tener más de 20 caracteres.",
                    result = ""
                });
            }

            // Verifica que se ingrese un valor para el stock
            if (producto.Stock == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad para el stock del producto",
                    result = ""
                });
            }

            // Verifica que el stock de producto sea mayor a 0
            if (producto.Stock <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad válida mayor a 0",
                    result = ""
                });
            }

            // Verifica que el stock sea un numero entero
            if (producto.Stock.ToString().Contains(".") || producto.Stock.ToString().Contains(","))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número entero",
                    result = ""
                });
            }

            // Verifica que se ingrese un valor para el precio unitario
            if (producto.PrecioUnitario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad para el precio unitario del producto",
                    result = ""
                });
            }

            // Verifica que el precio unitario sea mayor a 0
            if (producto.PrecioUnitario <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad válida mayor a 0",
                    result = ""
                });
            }

            // Verifica que el precio unitario sea un numero decimal
            if (producto.PrecioUnitario % 2 == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número decimal",
                    result = ""
                });
            }

            // Verifica que se ingrese un valor para la categoria
            if (producto.IdCategoria == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un id de la categoria a la que pertenece el producto",
                    result = ""
                });
            }

            // Verifica que el id de la categoria sea mayor a 0
            if (producto.IdCategoria <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un id válido mayor a 0",
                    result = ""
                });
            }

            // Verifica que la categoria sea un numero entero
            if (producto.IdCategoria.ToString().Contains(".") || producto.IdCategoria.ToString().Contains(","))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número entero",
                    result = ""
                });
            }

            if (producto.UrlImagen.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una url para la imagen",
                    result = ""
                });
            }

            // Verifica la longitud de la url de la imagen
            if (producto.UrlImagen.Length > 550)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una url más corta",
                    result = ""
                });
            }

            // Verifica que se ingrese una url
            if (producto.UrlImagen.Length == 0 || producto.UrlImagen.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una url de imagen para el producto",
                    result = ""
                });
            }

            // Verifica que se ingrese una descripcion
            if (producto.Descripcion.Length == 0 || producto.Descripcion.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una descripción para el producto",
                    result = ""
                });
            }

            // Verifica si existe un producto con el mismo nombre
            bool productoExistByName = _validations.ProductoExistByName(producto.Nombre);

            if (productoExistByName)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre del producto ya está en uso",
                    result = ""
                });
            }

            // Verifica si existe la categoría
            bool categoriaExist = _validations.CategoriaExist(producto.IdCategoria);

            if (!categoriaExist)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se ha encontrado la categoría",
                    result = ""
                });
            }

            // Llama al metodo del repositorio para guardar en la base de datos
            Producto product = new Producto();
            product.Nombre = producto.Nombre;
            product.Stock = producto.Stock;
            product.PrecioUnitario = producto.PrecioUnitario;
            product.UrlImagen = producto.UrlImagen;
            product.Descripcion = producto.Descripcion;
            product.CategoriaIdCategoria = producto.IdCategoria;
            product.Estado = 1;

            bool isAdd = _repo.Insert(product, producto.IdCategoria);

            if (!isAdd)
            {
                // Mensaje si no se guardo el producto
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ocurrió un error inesperado y no se pudo agregar el producto",
                    result = ""
                });
            }

            // Mensaje de confirmación
            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Producto agregado con éxito",
                result = ""
            });
        }

        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] ProductoDTO producto,int id)
        {
            // Verifica que se ingrese un valor para el nombre
            if (producto.Nombre.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un nombre para el producto",
                    result = ""
                });
            }

            // Verifica si el nombre del producto tiene más de 20 caracteres
            if (producto.Nombre.Length > 20)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre del producto no puede tener más de 20 caracteres.",
                    result = ""
                });
            }

            // Verifica que se ingrese un valor para el stock
            if (producto.Stock == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad para el stock del producto",
                    result = ""
                });
            }

            // Verifica que el stock de producto sea mayor a 0
            if (producto.Stock <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad válida mayor a 0",
                    result = ""
                });
            }

            // Verifica que el stock sea un numero entero
            if (producto.Stock.ToString().Contains(".") || producto.Stock.ToString().Contains(","))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número entero",
                    result = ""
                });
            }

            // Verifica que se ingrese un valor para el precio unitario
            if (producto.PrecioUnitario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad para el precio unitario del producto",
                    result = ""
                });
            }

            // Verifica que el precio unitario sea mayor a 0
            if (producto.PrecioUnitario <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una cantidad válida mayor a 0",
                    result = ""
                });
            }

            // Verifica que el precio unitario sea un numero decimal
            if (producto.PrecioUnitario % 2 == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número decimal",
                    result = ""
                });
            }

            // Verifica que se ingrese un valor para la categoria
            if (producto.IdCategoria == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un id de la categoria a la que pertenece el producto",
                    result = ""
                });
            }

            // Verifica que el id de la categoria sea mayor a 0
            if (producto.IdCategoria <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un id válido mayor a 0",
                    result = ""
                });
            }

            // Verifica que la categoria sea un numero entero
            if (producto.IdCategoria.ToString().Contains(".") || producto.IdCategoria.ToString().Contains(","))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese un número entero",
                    result = ""
                });
            }

            // Verifica que se ingrese una url
            if (producto.UrlImagen.Length == 0 || producto.UrlImagen.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una url de imagen para el producto",
                    result = ""
                });
            }

            // Verifica la longitud de la url de la imagen
            if (producto.UrlImagen.Length > 550)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una url más corta",
                    result = ""
                });
            }

            // Verifica que se ingrese una url
            if (producto.UrlImagen.Length == 0 || producto.UrlImagen.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una url de imagen para el producto",
                    result = ""
                });
            }

            // Verifica que se ingrese una descripcion
            if (producto.Descripcion.Length == 0 || producto.Descripcion.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese una descripción para el producto",
                    result = ""
                });
            }

            // Verifica si existe un producto con el mismo nombre
            bool productoExistByNameUpdate = _validations.ProductosExistUpdateByName(producto.Nombre);

            if (productoExistByNameUpdate)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre del producto ya está en uso",
                    result = ""
                });
            }

            bool categoriaExist = _validations.CategoriaExist(producto.IdCategoria);

            if (!categoriaExist)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se ha encontrado la categoría",
                    result = ""
                });
            }

            Producto product = new Producto();
            product.Nombre = producto.Nombre;
            product.Stock = producto.Stock;
            product.PrecioUnitario = producto.PrecioUnitario;
            product.UrlImagen = producto.UrlImagen;
            product.Descripcion = producto.Descripcion;
            product.CategoriaIdCategoria = producto.IdCategoria;
            product.Estado = 1;

            bool isUpdated = _repo.Update(product, id, producto.IdCategoria);

            if (!isUpdated)
            {
                // Mensaje si no se actualizo el producto
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Producto no encontrado",
                    result = ""
                });
            }

            // Mensaje de confirmación
            return StatusCode(StatusCodes.Status202Accepted, new
            {
                message = "Producto actualizado con éxito",
                result = ""
            });
        }

        [Authorize(Roles = "Administrador, Vendedor")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _repo.DeleteById(id);

            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Producto no encontrado",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status202Accepted, new
            {
                message = "Producto eliminado con éxito",
                result = ""
            });
        }
    }
}
