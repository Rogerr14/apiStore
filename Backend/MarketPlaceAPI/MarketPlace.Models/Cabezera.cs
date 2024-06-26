using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketPlace.Models;

public partial class Cabezera
{
    public int IdCompra { get; set; }

    public int? UsuarioIdUsuario { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Fecha { get; set; }

    public string? UsuarioCedula { get; set; }

    [JsonIgnore]
    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    [JsonIgnore]
    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; }
}
