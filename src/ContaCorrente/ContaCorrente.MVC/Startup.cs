using ContaCorrente.Repositorio.Interfaces;
using ContaCorrente.Repositorio.Model;
using ContaCorrente.Repositorio.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            ConfigureDataBase(services);

            ConfigureRepositorios(services);

            ConfigureDominios(services);

            services.AddControllersWithViews();
        }

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
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
            
        }

    }
}
