using Microsoft.AspNetCore.Mvc;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.API.Utilidad;
using SistemaAsociados.BLL.Servicios;

namespace SistemaAsociados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _deptoService;

        public DepartamentoController(IDepartamentoService deptoService)
        {
            _deptoService = deptoService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<DepartamentoDTO>>();
            try
            {
                response.status = true;
                response.value = await _deptoService.Lista();
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
        public async Task<IActionResult> Guardar([FromBody] DepartamentoDTO depto)
        {
            var response = new Response<DepartamentoDTO>();
            try
            {
                response.status = true;
                response.value = await _deptoService.Crear(depto);
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
        public async Task<IActionResult> Editar([FromBody] DepartamentoDTO depto)
        {
            var response = new Response<bool>();
            try
            {
                response.status = true;
                response.value = await _deptoService.Editar(depto);
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
