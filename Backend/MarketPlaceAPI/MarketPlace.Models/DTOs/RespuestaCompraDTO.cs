using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class RespuestaCompraDTO
    {
        public int status {  get; set; }
        public string message { get; set; }
        public ListCompraDTO? result { get; set; }
    }
}
