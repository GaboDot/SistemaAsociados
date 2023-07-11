using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaAsociados.BLL.Servicios;
using SistemaAsociados.BLL.Servicios.Contrato;
using SistemaAsociados.DAL.DBContext;
using SistemaAsociados.DAL.Repositories;
using SistemaAsociados.DAL.Repositories.Contrato;
using SistemaAsociados.Utils;

namespace SistemaAsociados.IOC
{
    public static class Dependencias
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configs)
        {
            services.AddDbContext<AsociadoSalarioContext>(options => {
                options.UseSqlServer(configs.GetConnectionString("SQLString"));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(AutomapperProfile));
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IDepartamentoService, DepartamentoService>();
            services.AddScoped<IAsociadoService, AsociadoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
