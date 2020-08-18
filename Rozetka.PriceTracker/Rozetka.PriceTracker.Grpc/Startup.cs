using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rozetka.PriceTracker.BackgroundWorkers;
using Rozetka.PriceTracker.EntityFramework.DbContext;
using Rozetka.PriceTracker.Grpc.Services;
using Rozetka.PriceTracker.Services.Options;
using Rozetka.PriceTracker.Services.ProductLoader;
using Rozetka.PriceTracker.Services.Products;
using Rozetka.PriceTracker.Services.ProductUpdater;
using System;

namespace Rozetka.PriceTracker.Grpc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<RozetkaDbContext>(options =>
             options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: action =>
                {
                    action.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                }));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                    });
            });
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.Configure<RozetkaOptions>(Configuration.GetSection("RozetkaOptions"));

            services.AddScoped<IProductLoaderService, ProductLoaderService>();
            services.AddScoped<IProductUpdaterService, ProductUpdaterService>();
            services.AddScoped<IProductsService, ProductsService>();

            services.AddHostedService<UpdateProductPricesBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();
            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<PriceTrackerService>().EnableGrpcWeb();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
