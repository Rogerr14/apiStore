﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Models.DTOs
{
    public class UsuarioDTO
    {
        public string? Cedula { get; set; }

        public string? NombreUsuario { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Contraseña { get; set; }
    }
}
