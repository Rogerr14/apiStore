using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketPlace.Models;

public partial class Detalle
{
    public int IdDetalle { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public int? ProductoIdProducto { get; set; }

    public int? CabezeraIdCompra { get; set; }

    [JsonIgnore]
    public virtual Cabezera? CabezeraIdCompraNavigation { get; set; }

    [JsonIgnore]
    public virtual Producto? ProductoIdProductoNavigation { get; set; }
}
