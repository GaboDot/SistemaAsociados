namespace SistemaAsociados.DTO
{
    public class SesionDTO
    {
        public int IdUsuario { get; set; }

        public int IdAsociado { get; set; }

        public string Nombre { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
