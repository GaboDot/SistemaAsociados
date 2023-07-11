using Microsoft.AspNetCore.Mvc;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.API.Utilidad;
using SistemaAsociados.BLL.Servicios;

namespace SistemaAsociados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsociadoController : ControllerBase
    {
        private readonly IAsociadoService _asociadoService;

        public AsociadoController(IAsociadoService asociadoService)
        {
            _asociadoService = asociadoService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<AsociadoDTO>>();
            try
            {
                response.status = true;
                response.value = await _asociadoService.Lista();
            }
            catch (Exception exc)
            {
                response.status = false;
                response.msg = exc.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Detalles")]
        public async Task<IActionResult> Detalles()
        {
            var response = new Response<List<AsociadoDetalleDTO>>();
            try
            {
                response.status = true;
                response.value = await _asociadoService.Detalles();
            }
            catch (Exception exc)
            {
                response.status = false;
                response.msg = exc.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] AsociadoDetalleDTO asociado)
        {
            var response = new Response<AsociadoDetalleDTO>();
            try
            {
                response.status = true;
                response.value = await _asociadoService.Crear(asociado);
            }
            catch (Exception exc)
            {
                response.status = false;
                response.msg = exc.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] AsociadoDetalleDTO asociado)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _asociadoService.Editar(asociado);
            }
            catch (Exception exc)
            {
                response.status = false;
                response.msg = exc.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _asociadoService.Eliminar(id);
            }
            catch (Exception exc)
            {
                response.status = false;
                response.msg = exc.Message;
            }
            return Ok(response);
        }
    }
}
