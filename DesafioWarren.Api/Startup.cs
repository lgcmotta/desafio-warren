using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Autofac;
using DesafioWarren.Api.Extensions;
using DesafioWarren.Application.Autofac;
using DesafioWarren.Infrastructure.EntityFramework;
using Microsoft.Identity.Web;

namespace DesafioWarren.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureContainer(ContainerBuilder container)
        {
            container.AddAutofacModules();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);
            
            services
                .AddAccountsDbContext(Configuration)
                .ConfigureCors()
                .ConfigureApiVersion()
                .AddRoutingWithLowerCaseUrls()
                .ConfigureSwaggerGen()
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json"
                    , "DesafioWarren.Api v1"));
            }

            app.UseCors()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
