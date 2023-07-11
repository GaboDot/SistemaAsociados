using AutoMapper;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DAL.Repositories.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.Model;

namespace SistemaAsociados.BLL.Servicios
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IGenericRepository<Departamento> _deptoRepository;
        private readonly IGenericRepository<Asociado> _asociadoRepository;
        private readonly IMapper _mapper;

        public DepartamentoService(
            IGenericRepository<Departamento> deptoRepo,
            IGenericRepository<Asociado>  asociadoRepo,
            IMapper mapper)
        {
            _deptoRepository = deptoRepo;
            _asociadoRepository = asociadoRepo;
            _mapper = mapper;
        }

        public async Task<List<DepartamentoDTO>> Lista()
        {
            try
            {
                var listaDeptos = await _deptoRepository.Consultar();
                return _mapper.Map<List<DepartamentoDTO>>(listaDeptos.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DepartamentoDTO> Crear(DepartamentoDTO model)
        {
            var deptoCreado = await _deptoRepository.Crear(_mapper.Map<Departamento>(model));
            if (deptoCreado.IdDepartamento == 0)
                throw new TaskCanceledException("No se pudo crear el usuario");
            return _mapper.Map<DepartamentoDTO>(deptoCreado);
        }

        public async Task<bool> Editar(DepartamentoDTO model)
        {
            var deptoModelo = _mapper.Map<Departamento>(model); 
            var deptoEncontrado= await _deptoRepository.Obtener(u => u.IdDepartamento == deptoModelo.IdDepartamento);
            var huboCambios = !deptoEncontrado.UltimoAumento.Equals(model.UltimoAumento);
            if (deptoEncontrado == null)
                throw new TaskCanceledException("No existe el usuario");

            deptoEncontrado.Nombre = deptoModelo.Nombre;
            deptoEncontrado.UltimoAumento = huboCambios ? model.UltimoAumento : deptoEncontrado.UltimoAumento;
            deptoEncontrado.Status = deptoModelo.Status;

            bool res = await _deptoRepository.Editar(deptoEncontrado);
            if (!res)
                throw new TaskCanceledException("No se pudo actualizar el usuario");

            if(huboCambios)
            {
                List<Asociado> asociadosXdepto = (await _asociadoRepository.Consultar(u => u.FkIdDepartamento == deptoModelo.IdDepartamento)).ToList();
                double aumento = 1 + double.Parse(deptoModelo.UltimoAumento);
                foreach (Asociado asociado in asociadosXdepto)
                {
                    double salarioActual = double.Parse(asociado.Salario.ToString());
                    double salarioNuevo = salarioActual * aumento;
                    asociado.Salario = decimal.Parse(salarioNuevo.ToString());

                    res = await _asociadoRepository.Editar(asociado);
                }
            }
            return res;
        }

        public async Task<bool> Eliminar(int id)
        {
            var deptoEncontrado = await _deptoRepository.Obtener(u => u.IdDepartamento == id);
            if (deptoEncontrado == null)
                throw new TaskCanceledException("No existe el usuario");
            bool res = await _deptoRepository.Eliminar(deptoEncontrado);
            if (!res)
                throw new TaskCanceledException("No se pudo eliminar el usuario");

            return res;
        }
    }
}
