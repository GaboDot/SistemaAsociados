using System;
using System.Collections.Generic;

namespace SistemaAsociados.Model;

public partial class Asociado
{
    public int IdAsociado { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public int Status { get; set; }

    public decimal Salario { get; set; }

    public DateTime FechaIngreso { get; set; }

    public int FkIdDepartamento { get; set; }

    public virtual Departamento FkIdDepartamentoNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
