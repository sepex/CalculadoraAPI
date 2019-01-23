using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Calculadora.API.Model.Settings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Calculadora.API
{
  public class Startup
  {
    [ExcludeFromCodeCoverage]
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    [ExcludeFromCodeCoverage]
    public IConfiguration Configuration { get; }

    [ExcludeFromCodeCoverage]
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info
        {
          Title = "Calculadora de Juros",
          Description= @"Este projeto pode ser clonado a partir do repositório Git disponível do projeto (link abaixo).",
          Version = "v1",
          Contact = new Contact()
          {
            Email = "lucaspsepe@gmail.com",
            Name = "Lucas Peixoto Sepe",
            Url = "https://github.com/sepex/CalculadoraAPI"// TODO - Alterar para o projeto Git
          }
        });
      });

      services.Configure<DefaultSettings>(Configuration.GetSection("Defaults"));

      services.AddMediatR();

    }

    [ExcludeFromCodeCoverage]
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Calculadora de Juros V1");
        c.RoutePrefix = string.Empty;
      });

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
