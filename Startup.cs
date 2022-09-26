using CQRS_Demo.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Microsoft.OpenApi.Models;
using CQRS_Demo.ProductFeatures.Queries;
using CQRS_Demo.ProductFeatures.Commands;

namespace CQRS_Demo
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
      services.AddControllers();

      services.AddDbContext<ApplicationContext>(options =>     options.UseSqlServer(Configuration.GetConnectionString("CQRS")));
      services.AddMediatR(typeof(ApplicationContext).GetTypeInfo().Assembly);
      services.AddMediatR(Assembly.GetExecutingAssembly());
      services.AddSwaggerGen();

      services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());
      //services.AddScoped(provider => provider.GetServices<GetAllProductsQuery>());
      //services.AddScoped(provider => provider.GetServices<GetProductByIdQuery>());
      //services.AddScoped(provider => provider.GetServices<CreateProductCommand>());
      //services.AddScoped(provider => provider.GetServices<UpdateProductCommand>());
      //services.AddScoped(provider => provider.GetServices<DeleteProductByIdCommand>());
     // services.AddMediatR(typeof(LibraryEntrypoint).Assembly);

      services.AddMediatR(Assembly.GetExecutingAssembly());

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS API");
        });
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
