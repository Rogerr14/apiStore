using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class ListCompraDTO
    {
        public int? IdCompra { get; set; }

        public int? UsuarioIdUsuario { get; set; }

        public decimal? Total { get; set; }

        public DateTime? Fecha { get; set; }

        public string? UsuarioCedula { get; set; }

        public List<ListDetalleDTO>? Detalles { get; set; }
    }
}
