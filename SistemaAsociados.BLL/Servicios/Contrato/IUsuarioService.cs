using SistemaAsociados.DTO;

namespace SistemaAsociados.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();

        Task<SesionDTO> ValidarCredenciales(string email, string pwd);

        Task<UsuarioDTO> Crear(UsuarioDTO model);

        Task<bool> Editar(UsuarioDTO model);

        Task<bool> Eliminar(int id);
    }
}
