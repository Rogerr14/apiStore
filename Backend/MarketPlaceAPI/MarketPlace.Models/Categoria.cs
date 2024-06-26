using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketPlace.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public int? Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
