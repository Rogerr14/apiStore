using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketPlace.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Cedula { get; set; }

    public string? NombreUsuario { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Contraseña { get; set; }

    public int? Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cabezera> Cabezeras { get; set; } = new List<Cabezera>();
}
