using DatabaseRepository.Factories.ContextFactory;
using DatabaseRepository.Repositories;
using DatabaseRepository.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            var connectionString = AppConfiguration["ConnectionStrings:DefaultConnection"];
            services.AddScoped<IArticleRepository>(provider => new ArticleRepository(
                    connectionString,
                    provider.GetService<IRepositoryContextFactory>()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
