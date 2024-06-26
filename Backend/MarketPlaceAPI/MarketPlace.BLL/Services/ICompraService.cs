using Azure;
using MarketPlace.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Services
{
    public interface ICompraService
    {
        public RespuestaCompraDTO ProcesarCompra(CompraDTO compra);
    }
}
