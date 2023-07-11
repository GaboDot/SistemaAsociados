namespace SistemaAsociados.DTO
{
    public class DepartamentoDTO
    {
        public int IdDepartamento { get; set; }

        public string Nombre { get; set; } = null!;

        public int Status { get; set; }

        public string UltimoAumento { get; set; }
    }
}
