using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DAL.Repositories.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.Model;
using SistemaAsociados.Utils;
using System.Runtime.InteropServices.ComTypes;

namespace SistemaAsociados.BLL.Servicios
{
    public class AsociadoService : IAsociadoService
    {
        private readonly IGenericRepository<Asociado> _asociadoRepository;
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;
        string hashed = "";

        public AsociadoService(
            IGenericRepository<Asociado> asociadoRepository,
            IGenericRepository<Usuario> usuarioRepository,
            IMapper mapper)
        {
            _asociadoRepository = asociadoRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<AsociadoDTO>> Lista()
        {
            try
            {
                var queryAsociados = await _asociadoRepository.Consultar();
                var listaUsuarios = queryAsociados.Include(depto => depto.FkIdDepartamentoNavigation).ToList();
                return _mapper.Map<List<AsociadoDTO>>(listaUsuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AsociadoDetalleDTO>> Detalles()
        {
            List<AsociadoDetalleDTO> lista = new List<AsociadoDetalleDTO> ();
            List<Asociado> tblAsociado = (await _asociadoRepository.Consultar()).ToList();
            List<Usuario> tblUsuario = (await _usuarioRepository.Consultar()).ToList();
            try
            {
                foreach (Asociado asc in tblAsociado)
                {
                    foreach (Usuario usuario in tblUsuario)
                    {
                        if(usuario.FkIdAsociado == asc.IdAsociado)
                        {
                            AsociadoDetalleDTO aux = new AsociadoDetalleDTO
                            {
                                IdAsociado = asc.IdAsociado,
                                IdUsuario = usuario.IdUsuario,
                                IdDepartamento = asc.FkIdDepartamento,
                                Nombre = asc.Nombre,
                                ApellidoPaterno = asc.ApellidoPaterno,
                                ApellidoMaterno = asc.ApellidoMaterno,
                                Salario = asc.Salario,
                                FechaIngreso = asc.FechaIngreso,
                                Email = usuario.Email,
                                Clave = usuario.Clave,
                                Status = asc.Status
                            };
                            lista.Add(aux);
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AsociadoDetalleDTO> Crear(AsociadoDetalleDTO model)
        {

            hashed = HashPassword.CreateSHAHash(model.Clave);
            try
            {
                AsociadoDTO nuevoAsociado = new AsociadoDTO { 
                    Nombre = model.Nombre,
                    ApellidoPaterno=model.ApellidoPaterno,
                    ApellidoMaterno=model.ApellidoMaterno,
                    Salario = model.Salario,
                    Status = model.Status,
                    FkIdDepartamento = model.IdDepartamento
                };

                UsuarioDTO nuevoUsuario = new UsuarioDTO { 
                    Email = model.Email,
                    Clave = hashed,
                };

                var asociadoCreado = await _asociadoRepository.Crear(_mapper.Map<Asociado>(nuevoAsociado));
                if (asociadoCreado.IdAsociado == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                var query = await _asociadoRepository.Consultar(u => u.IdAsociado == asociadoCreado.IdAsociado);
                asociadoCreado = query.Include(u => u.FkIdDepartamentoNavigation).First();

                nuevoUsuario.FkIdAsociado = asociadoCreado.IdAsociado;

                var usuarioCreado = await _usuarioRepository.Crear(_mapper.Map<Usuario>(nuevoUsuario));
                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                var query2 = await _usuarioRepository.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query2.Include(u => u.FkIdAsociadoNavigation).First();

                AsociadoDetalleDTO detalleCreado = new AsociadoDetalleDTO {
                    IdAsociado = asociadoCreado.IdAsociado,
                    IdUsuario = usuarioCreado.IdUsuario,
                    IdDepartamento = asociadoCreado.FkIdDepartamento,
                    Nombre = asociadoCreado.Nombre,
                    ApellidoPaterno = asociadoCreado.ApellidoPaterno,
                    ApellidoMaterno = asociadoCreado.ApellidoMaterno,
                    Salario = asociadoCreado.Salario,
                    FechaIngreso = asociadoCreado.FechaIngreso,
                    Email = usuarioCreado.Email,
                    Clave = usuarioCreado.Clave,
                    Status = asociadoCreado.Status
                };

                return _mapper.Map<AsociadoDetalleDTO>(detalleCreado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(AsociadoDetalleDTO model)
        {
            hashed = HashPassword.CreateSHAHash(model.Clave);
            try
            {
                var asociadoModelo = _mapper.Map<AsociadoDetalleDTO>(model);
                var asociadoEncontrado = await _asociadoRepository.Obtener(u => u.IdAsociado == asociadoModelo.IdAsociado);
                if (asociadoEncontrado == null)
                    throw new TaskCanceledException("No existe el usuario");

                asociadoEncontrado.Nombre = asociadoModelo.Nombre;
                asociadoEncontrado.ApellidoPaterno = asociadoModelo.ApellidoPaterno;
                asociadoEncontrado.ApellidoMaterno = asociadoModelo.ApellidoMaterno;
                asociadoEncontrado.FkIdDepartamento = asociadoModelo.IdDepartamento;
                asociadoEncontrado.Status = asociadoModelo.Status;
                bool res = await _asociadoRepository.Editar(asociadoEncontrado);
                if (!res)
                    throw new TaskCanceledException("No se pudo actualizar el usuario");

                var usuarioEncontrado = await _usuarioRepository.Obtener(u => u.IdUsuario == asociadoModelo.IdUsuario);
                if (asociadoEncontrado == null)
                    throw new TaskCanceledException("No existe el usuario");

                usuarioEncontrado.Email = asociadoModelo.Email;
                usuarioEncontrado.Clave = hashed;
                usuarioEncontrado.Status = asociadoModelo.Status;

                res = await _usuarioRepository.Editar(usuarioEncontrado);
                if (!res)
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
                var usuarioEncontrado = await _asociadoRepository.Obtener(u => u.IdAsociado == id);
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("No existe el usuario");
                bool res = await _asociadoRepository.Eliminar(usuarioEncontrado);
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
