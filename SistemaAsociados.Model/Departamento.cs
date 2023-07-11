using System;
using System.Collections.Generic;

namespace SistemaAsociados.Model;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int Status { get; set; }

    public string UltimoAumento { get; set; }

    public virtual ICollection<Asociado> Asociados { get; set; } = new List<Asociado>();
}
