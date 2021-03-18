using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Autofac;
using DesafioWarren.Api.Extensions;
using DesafioWarren.Application.Autofac;
using DesafioWarren.Application.AutoMapper;
using DesafioWarren.Application.Serilog;
using DesafioWarren.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.Identity.Web;

namespace DesafioWarren.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string ApplicationAssembly = "DesafioWarren.Application";

        private const string DomainAssembly = "DesafioWarren.Domain";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureContainer(ContainerBuilder container)
        {
            container.AddAutofacModules(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);
            
            services
                .AddHttpContextAccessor()
                .ConfigureSerilog(Configuration)
                .AddAutoMapperFromAssemblies(ApplicationAssembly)
                .AddAccountsDbContext(Configuration)
                //.AddMediatR(Assembly.Load(ApplicationAssembly), Assembly.Load(DomainAssembly))
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
