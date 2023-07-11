using System;
using System.Collections.Generic;

namespace SistemaAsociados.Model;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public int FkIdAsociado { get; set; }

    public int Status { get; set; }

    public virtual Asociado FkIdAsociadoNavigation { get; set; } = null!;
}
