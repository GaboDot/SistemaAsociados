using SistemaAsociados.DTO;

namespace SistemaAsociados.BLL.Servicios.Contrato
{
    public interface IAsociadoService
    {
        Task<List<AsociadoDTO>> Lista();

        Task<List<AsociadoDetalleDTO>> Detalles();

        Task<AsociadoDetalleDTO> Crear(AsociadoDetalleDTO model);

        Task<bool> Editar(AsociadoDetalleDTO model);

        Task<bool> Eliminar(int id);
    }
}
