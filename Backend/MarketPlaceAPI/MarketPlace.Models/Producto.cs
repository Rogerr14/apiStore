using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketPlace.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public int? Stock { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? CategoriaIdCategoria { get; set; }

    public int? Estado { get; set; }

    public string? UrlImagen { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    [JsonIgnore]
    public virtual Categoria? CategoriaIdCategoriaNavigation { get; set; }
}
