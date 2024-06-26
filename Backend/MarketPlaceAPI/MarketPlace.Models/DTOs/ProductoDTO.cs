using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class ProductoDTO
    {
        public string? Nombre { get; set; }

        public int? Stock { get; set; }

        public decimal? PrecioUnitario { get; set; }

        public string? UrlImagen { get; set; }

        public string? Descripcion { get; set; }

        public int? IdCategoria { get; set; }

        public int? IdVendedor { get; set; }    

    }
}
