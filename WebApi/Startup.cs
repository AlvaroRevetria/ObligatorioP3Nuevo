using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
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
            services.AddControllers()
                 .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                                                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();

            services.AddScoped<IRepositorioSelecciones, RepositorioSelecciones>();
            services.AddScoped<IRepositorioGrupos, RepositorioGrupos>();
            services.AddScoped<IRepositorioPartidos, RepositorioPartidos>();
            services.AddScoped<IRepositorioPaises, RepositorioPaises>();
            services.AddScoped<IRepositorioGrupos, RepositorioGrupos>();


            string strCon = Configuration.GetConnectionString("ConeccionAlvaro");
            services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(strCon));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
