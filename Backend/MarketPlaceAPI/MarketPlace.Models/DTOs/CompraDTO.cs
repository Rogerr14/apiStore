using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class CompraDTO
    {
        public int? UsuarioIdUsuario { get; set; }

        public List<DetalleDTO>? Detalles { get; set; }
    }
}
