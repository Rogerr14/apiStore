using MarketPlace.DAL.DataContext;
using MarketPlace.Models;
using MarketPlace.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Services
{
    public class CompraService : ICompraService
    {
        private readonly DbMarketPlaceContext _context;

        public CompraService(DbMarketPlaceContext context)
        {
            _context = context;
        }

        // Método que devuelve true o false aleatoriamente
        private bool ValidarSaldo()
        {
            Random random = new Random();

            int numeroRandom = random.Next(0, 2);

            return numeroRandom % 2 == 0;
        }

        public RespuestaCompraDTO ProcesarCompra(CompraDTO compra)
        {
            try
            {
                if (!ValidarSaldo()) return new RespuestaCompraDTO() { status = 400, message = "Saldo insuficiente", result = null };

                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == compra.UsuarioIdUsuario);

                if (usuario == null) return new RespuestaCompraDTO() { status = 404, message = "Usuario no encontrado", result = null };

                decimal? total = 0;
                List<Detalle> detalles = new List<Detalle>();

                // Primero resta el stock de los productos
                foreach (var producto in compra.Detalles)
                {
                    var actualProduct = _context.Productos.FirstOrDefault(p => p.IdProducto == producto.ProductoIdProducto && p.Estado == 1);

                    if (actualProduct != null && actualProduct.Stock > 0)
                    {
                        actualProduct.Stock -= producto.Cantidad;

                        total += producto.Cantidad * actualProduct.PrecioUnitario;

                        if (actualProduct.Stock == 0)
                        {
                            actualProduct.Estado = 0;
                        }
                    }
                    else
                    {
                        return new RespuestaCompraDTO() { status = 404, message = "Producto no disponible", result = null };
                    }
                }

                // Segundo guarda la cabezera
                Cabezera ordenCompra = new Cabezera();
                ordenCompra.UsuarioIdUsuario = usuario.IdUsuario;
                ordenCompra.Total = total;
                ordenCompra.Fecha = DateTime.Now.Date;
                ordenCompra.UsuarioCedula = usuario.Cedula;

                _context.Cabezeras.Add(ordenCompra);
                _context.SaveChanges();

                // Tercero recorre los productos de los detalles de la compra y los guarda
                foreach (var producto in compra.Detalles)
                {
                    var actualProduct = _context.Productos.FirstOrDefault(p => p.IdProducto == producto.ProductoIdProducto);

                    Detalle detalleProducto = new Detalle();
                    detalleProducto.Cantidad = producto.Cantidad;
                    detalleProducto.Total = producto.Cantidad * actualProduct.PrecioUnitario;
                    detalleProducto.ProductoIdProducto = producto.ProductoIdProducto;
                    detalleProducto.CabezeraIdCompra = ordenCompra.IdCompra;

                    detalles.Add(detalleProducto);
                    _context.Add(detalleProducto);
                }

                _context.SaveChanges();

                ListCompraDTO resultado = new ListCompraDTO();
                resultado.IdCompra = ordenCompra.IdCompra;
                resultado.UsuarioIdUsuario = ordenCompra.UsuarioIdUsuario;
                resultado.Total = ordenCompra.Total;
                resultado.Fecha = ordenCompra.Fecha;
                resultado.UsuarioCedula = ordenCompra.UsuarioCedula;
                resultado.Detalles = new List<ListDetalleDTO>();

                foreach (var detalleProducto in detalles)
                {
                    ListDetalleDTO listDetalle = new ListDetalleDTO();
                    listDetalle.Cantidad = detalleProducto.Cantidad;
                    listDetalle.Total = detalleProducto.Total;
                    listDetalle.ProductoIdProducto = detalleProducto.ProductoIdProducto;
                    listDetalle.CabezeraIdCompra = detalleProducto.CabezeraIdCompra;

                    resultado.Detalles.Add(listDetalle);
                }

                return new RespuestaCompraDTO() { status = 200, message = "Compra realizada con éxito", result = resultado };
            }
            catch (Exception)
            {
                return new RespuestaCompraDTO() { status = 400, message = "Error al crear la compra", result = null };
            }
        }
    }
}
