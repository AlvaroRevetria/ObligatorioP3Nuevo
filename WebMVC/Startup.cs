using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogicaNegocio.InterfacesRepositorios;
using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais;
using LogicaAplicacion.CasosUso.CasosUsoPais;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoRegion;
using LogicaAplicacion.CasosUso.CasosUsoRegion;
using LogicaAccesoDatos.BaseDatos;
using Microsoft.EntityFrameworkCore;
using LogicaNegocio.Dominio;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoGrupos;
using LogicaAplicacion.CasosUso;

namespace WebMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //Contexto EntityFramework
            string strCon = Configuration.GetConnectionString("ConeccionAna");
            services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(strCon));
            // INYECCIONES PARA CONTROLLERS(CU)
            services.AddScoped<IAltaPais, AltaPais>();
            services.AddScoped<IActualizarPais, ActualizarPais>();
            services.AddScoped<IBajaPais, BajaPais>();
            services.AddScoped<IBuscarPaisesPorRegion, BuscarPaisesPorRegion>();
            services.AddScoped<IBuscarPaisPorCodigoAlfa, BuscarPaisPorCodAlfa>();
            services.AddScoped<IObtenerPais, ObtenerPais>();
            services.AddScoped<IObtenerPaises, ObtenerPaises >();
            services.AddScoped<IObtenerGrupos, ObtenerGrupos>();
            services.AddScoped<IObtenerRegiones, ObtenerRegiones>();



            //INYECCIONES PARA CU (REPOS)
            services.AddScoped<IRepositorioPaises, RepositorioPaises>();
            services.AddScoped<IRepositorioRegiones, RepositorioRegiones>();
            services.AddScoped<IRepositorioSelecciones, RepositorioSelecciones>();
            services.AddScoped<IRepositorioGrupos, RepositorioGrupos>();

            services.AddSession();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Usuarios}/{action=Login}/{id?}");
            });
        }
    }
}
