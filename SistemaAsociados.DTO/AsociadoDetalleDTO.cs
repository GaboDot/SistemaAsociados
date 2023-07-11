namespace SistemaAsociados.DTO
{
    public class AsociadoDetalleDTO
    {
        public int IdAsociado { get; set; }

        public int IdUsuario { get; set; }

        public int IdDepartamento { get; set; }

        public string Nombre { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public decimal Salario { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string Email { get; set; } = null!;

        public string Clave { get; set; } = null!;

        public int Status { get; set; }

        
    }
}
