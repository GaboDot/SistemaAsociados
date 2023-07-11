using AutoMapper;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DAL.Repositories.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.Model;

namespace SistemaAsociados.BLL.Servicios
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista()
        {
            try
            {
                var listaMenu = await _menuRepository.Consultar();
                return _mapper.Map<List<MenuDTO>>(listaMenu.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
