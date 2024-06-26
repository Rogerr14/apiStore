using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class CabezeraDTO
    {
        public int? UsuarioIdUsuario { get; set; }

        public decimal? Total { get; set; }

        public DateTime? Fecha { get; set; }

        public string? UsuarioCedula { get; set; }
    }
}
