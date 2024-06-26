using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class ListDetalleDTO
    {
        public int? Cantidad { get; set; }

        public decimal? Total { get; set; }

        public int? ProductoIdProducto { get; set; }

        public int? CabezeraIdCompra { get; set; }
    }
}
