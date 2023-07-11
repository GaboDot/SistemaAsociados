using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DAL.Repositories.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.Model;
using SistemaAsociados.Utils;
using System.Text;

namespace SistemaAsociados.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<Asociado> _asociadoRepository;
        private readonly IMapper _mapper;
        string hashed = "";

        public UsuarioService(
            IGenericRepository<Usuario> usuarioRepository,
            IGenericRepository<Asociado> asociadoRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _asociadoRepository = asociadoRepository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepository.Consultar();
                var listaUsuarios = queryUsuario.Include(asc => asc.FkIdAsociadoNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SesionDTO> ValidarCredenciales(string email, string pwd)
        {
            try
            {
                string base64EncodedString = pwd;
                string decodedPwd = Encoding.GetEncoding(28591).GetString(Convert.FromBase64String(base64EncodedString));
                hashed = HashPassword.CreateSHAHash(decodedPwd);
                var queryUsuario = await _usuarioRepository.Consultar(u =>
                    u.Email == email &&
                    u.Clave == hashed);
                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                Usuario devolverUsuario = queryUsuario.Include(u => u.FkIdAsociadoNavigation).First();

                var queryAsociado = await _asociadoRepository.Consultar(asc => asc.IdAsociado == devolverUsuario.FkIdAsociado);
                if (queryAsociado.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                Asociado devolverAsociado = queryAsociado.Include(asc => asc.FkIdDepartamentoNavigation).First();

                SesionDTO sesionActual = new SesionDTO();
                sesionActual.IdUsuario = devolverUsuario.IdUsuario;
                sesionActual.IdAsociado = devolverAsociado.IdAsociado;
                sesionActual.Nombre = devolverAsociado.Nombre;
                sesionActual.ApellidoPaterno = devolverAsociado.ApellidoPaterno;
                sesionActual.Email = devolverUsuario.Email;

                return sesionActual;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO model)
        {
            try
            {
                var usuarioCreado = await _usuarioRepository.Crear(_mapper.Map<Usuario>(model));
                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                var query = await _usuarioRepository.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query.Include(asc => asc.FkIdAsociadoNavigation).First();
                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO model)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(model);
                var usuarioEncontrado = await _usuarioRepository.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);
                if(usuarioEncontrado == null)
                    throw new TaskCanceledException("No existe el usuario");

                usuarioEncontrado.Email = usuarioModelo.Email;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.Status = usuarioModelo.Status;

                bool res = await _usuarioRepository.Editar(usuarioEncontrado);
                if(!res)
                    throw new TaskCanceledException("No se pudo actualizar el usuario");

                return res;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepository.Obtener(u => u.IdUsuario == id);
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("No existe el usuario");
                bool res = await _usuarioRepository.Eliminar(usuarioEncontrado);
                if (!res)
                    throw new TaskCanceledException("No se pudo eliminar el usuario");

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
