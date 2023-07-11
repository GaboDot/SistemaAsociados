namespace SistemaAsociados.DTO
{
    public class AsociadoDTO
    {
        public int IdAsociado { get; set; }

        public string Nombre { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public int Status { get; set; }

        public decimal Salario { get; set; }

        public int FkIdDepartamento { get; set; }

        public DateTime FechaIngreso { get; set; }
    }
}
