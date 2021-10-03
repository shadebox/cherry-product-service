#region Include Definition
using System;
using System.IO;
using ProductService.Database.Domain;
using Microsoft.EntityFrameworkCore;
using ProductService.Database.EFConfiguration;
using Microsoft.Extensions.Configuration;
#endregion

namespace ProductService.Database.DBContext
{
    #region Public Class Definition
    public sealed class EFContext : DbContext
    {
        #region Private Field Definition
        private readonly string _defaultEnvironment;
        #endregion

        #region Public Property Definition
        public IConfiguration Configuration { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

        #region Public Constructor Definition
        public EFContext()
        {
            // Disable the auto transaction for sql server database
            // We do not need to enable this as EFRepository class
            // will handle this
            Database.AutoTransactionsEnabled = false;

            // Get the environment this application is running on
            _defaultEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<EFContext>()
                .Build();
        }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            // Get the environment this application is running on
            _defaultEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<EFContext>()
                .Build();
        }
        #endregion

        #region Public Method Definition
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Setting database connection string
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString(_defaultEnvironment), 
                    providerOptions => providerOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null)).EnableDetailedErrors(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
    #endregion
}