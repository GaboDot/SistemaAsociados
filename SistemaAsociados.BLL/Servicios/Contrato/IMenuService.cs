using SistemaAsociados.DTO;

namespace SistemaAsociados.BLL.Servicios.Contrato
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> Lista();
    }
}
