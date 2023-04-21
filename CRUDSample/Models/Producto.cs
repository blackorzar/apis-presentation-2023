using System;
using System.Collections.Generic;

namespace CrudSample.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Costo { get; set; }

    public decimal? Precio { get; set; }
}
