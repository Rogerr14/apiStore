using MarketPlace.BLL.Services;
using MarketPlace.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Comprar(CompraDTO ordenCompra)
        {
            // Verifica que se ingrese un valor para el id del usuario
            if (ordenCompra.UsuarioIdUsuario <= 0 || ordenCompra.UsuarioIdUsuario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El id del usuario es necesario",
                    result = ""
                });
            }

            // Verifica que el id del usuario sea un número entero
            if (ordenCompra.UsuarioIdUsuario.ToString().Contains(".") || ordenCompra.UsuarioIdUsuario.ToString().Contains(","))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El id del usuario debe ser un número entero",
                    result = ""
                });
            }

            // Verifica si se agregó al menos 1 producto
            if (ordenCompra.Detalles.Count == 0 || ordenCompra.Detalles.IsNullOrEmpty())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Ingrese al menos un producto",
                    result = ""
                });
            }

            foreach (var detalleProducto in ordenCompra.Detalles)
            {
                // Verifica que se ingrese un valor para la cantidad
                if (detalleProducto.Cantidad <= 0 || detalleProducto.Cantidad == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        message = "La cantidad es necesaria",
                        result = ""
                    });
                }

                // Verifica que la cantidad sea un número entero
                if (detalleProducto.Cantidad.ToString().Contains(".") || detalleProducto.Cantidad.ToString().Contains(","))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        message = "La cantidad debe ser un número entero",
                        result = ""
                    });
                }

                // Verifica que se ingrese un valor para el id del producto
                if (detalleProducto.ProductoIdProducto <= 0 || detalleProducto.ProductoIdProducto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        message = "El producto es necesario",
                        result = ""
                    });
                }

                // Verifica que el id del producto sea un número entero
                if (detalleProducto.ProductoIdProducto.ToString().Contains(".") || detalleProducto.ProductoIdProducto.ToString().Contains(","))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        message = "El id del producto ser un número entero",
                        result = ""
                    });
                }
            }

            // Llama al metodo del servicio para procesar la compra
            var mensajeRespuesta = _compraService.ProcesarCompra(ordenCompra);

            // Respuestas segun el estado devuelto del servicio
            if (mensajeRespuesta.status == 400)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = mensajeRespuesta.message,
                    result = mensajeRespuesta.result
                });
            }

            if (mensajeRespuesta.status == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = mensajeRespuesta.message,
                    result = mensajeRespuesta.result
                });
            }

            if (mensajeRespuesta.status == 200)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    message = mensajeRespuesta.message, 
                    result = mensajeRespuesta.result // Devolver la compra con cabezera y detalles para generar el pdf de la factura
                });
            }

            return StatusCode(StatusCodes.Status400BadRequest, new
            {
                message = "Error al crear la compra",
                result = mensajeRespuesta.result
            });
        }
    }
}
