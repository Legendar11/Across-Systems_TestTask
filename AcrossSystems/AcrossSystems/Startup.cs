using DatabaseRepository.Factories.ContextFactory;
using DatabaseRepository.Repositories;
using DatabaseRepository.Repositories.Interfaces;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React.AspNet;

namespace AcrossSystems
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            AppConfiguration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            services.AddJsEngineSwitcher(options => 
                options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();

            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            var connectionString = AppConfiguration["ConnectionStrings:DefaultConnection"];
            services.AddScoped<IArticleRepository>(provider => new ArticleRepository(
                    connectionString,
                    provider.GetService<IRepositoryContextFactory>()));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseReact(config => { });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
