using System;
using System.Collections.Generic;

namespace SistemaAsociados.Model;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string Etiqueta { get; set; } = null!;

    public string Icono { get; set; } = null!;

    public string Url { get; set; } = null!;
}
