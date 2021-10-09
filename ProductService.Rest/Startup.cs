#region Include Definition
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Repository.IRepository;
using ProductService.Repository.EFRepository;
using ProductService.BusinessLogic.Service;
using ProductService.Database.DBContext;
using Microsoft.EntityFrameworkCore;
#endregion

namespace ProductService.Rest
{
    #region Public Class Definition
    public class Startup
    {
        #region Private Field Definition
        private readonly string _policyName;
        private readonly string _defaultEnvironment;
        #endregion

        #region Public Property Definition
        public IConfiguration Configuration { get; set; }
        #endregion
        
        #region Public Constructor Definition
        public Startup(IConfiguration configuration)
        {
            _policyName = "CorsPolicy";
            _defaultEnvironment = "Development";

            Configuration = configuration;
        }
        #endregion

        #region Public Method Definition
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? _defaultEnvironment;

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder
                        //.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddDbContext<EFContext>(options => options.UseSqlServer(Configuration.GetConnectionString(environment)));

            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<IProductService, BusinessLogic.Service.ProductService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Display detailed errors for development
                app.UseDeveloperExceptionPage();

                //Displays 404 error message
                app.UseStatusCodePages();
            }
            else
            {
                // HTTP Strict Transport Security
                app.UseHsts();

                // HTTP redirection to HTTPS
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            // Use at controller or action level instead [EnableCors(_policyName)]
            //app.UseCors(_policyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion
    }
    #endregion
}
