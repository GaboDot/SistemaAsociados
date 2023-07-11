using Microsoft.AspNetCore.Mvc;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DTO;
using SistemaAsociados.API.Utilidad;

namespace SistemaAsociados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<MenuDTO>>();
            try
            {
                response.status = true;
                response.value = await _menuService.Lista();
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
