namespace SistemaAsociados.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Email { get; set; } = null!;

        public string Clave { get; set; } = null!;

        public int Status { get; set; }

        public int FkIdAsociado { get; set; }
    }
}
