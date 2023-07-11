using SistemaAsociados.DTO;

namespace SistemaAsociados.BLL.Servicios.Contrato
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDTO>> Lista();

        Task<DepartamentoDTO> Crear(DepartamentoDTO model);

        Task<bool> Editar(DepartamentoDTO model);

        Task<bool> Eliminar(int id);
    }
}
