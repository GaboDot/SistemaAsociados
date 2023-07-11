namespace SistemaAsociados.DTO
{
    public class MenuDTO
    {
        public int IdMenu { get; set; }

        public string Etiqueta { get; set; } = null!;

        public string Icono { get; set; } = null!;

        public string Url { get; set; } = null!;
    }
}
