using AutoMapper;
using SistemaAsociados.DTO;
using SistemaAsociados.Model;

namespace SistemaAsociados.Utils
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Asociado
            CreateMap<Asociado, AsociadoDTO>().ReverseMap();
            #endregion

            #region Departamento
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            #endregion

            #region Sesion
            CreateMap<Usuario, SesionDTO>().ReverseMap();
            CreateMap<Asociado, SesionDTO>().ReverseMap();
            #endregion
        }
    }
}
