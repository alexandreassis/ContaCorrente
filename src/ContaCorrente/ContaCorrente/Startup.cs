using ContaCorrente.Dominio.Dominios;
using ContaCorrente.Dominio.Interfaces;
using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using ContaCorrente.Repositorio.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContaCorrente
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            ConfigureDataBase(services);

            ConfigureRepositorios(services);

            ConfigureDominios(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void ConfigureDataBase(IServiceCollection services)
        {
            services.AddDbContext<CCDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CCDatabase")));

            services.AddScoped<CCDbContext>();
        }

        private void ConfigureRepositorios(IServiceCollection services)
        {
            services.AddScoped<ITipoTransacaoRepositorio, TipoTransacaoRepositorio>();
            services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
            services.AddScoped<IContaRepositorio, ContaRepositorio>();
            services.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();
            services.AddScoped<IRendimentoDiarioCCRepositorio, RendimentoDiarioCCRepositorio>();
        }

        private void ConfigureDominios(IServiceCollection services)
        {
            services.AddScoped<IPessoaDominio, PessoaDominio>();
            services.AddScoped<IContaDominio, ContaDominio>();
            services.AddScoped<ITransacaoDominio, TransacaoDominio>();
            services.AddScoped<IRendimentoDiarioDominio, RendimentoDiarioDominio>();
        }

    }
}
