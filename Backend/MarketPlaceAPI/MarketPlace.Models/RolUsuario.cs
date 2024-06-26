using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarketPlace.Models;

public partial class RolUsuario
{
    public int? RolIdRol { get; set; }

    [Key]
    public int? UsuarioIdUsuario { get; set; }

    [JsonIgnore]
    public virtual Rol? RolIdRolNavigation { get; set; }

    [JsonIgnore]
    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; }
}
