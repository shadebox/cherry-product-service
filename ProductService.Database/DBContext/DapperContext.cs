#region Include Definition
using System.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
#endregion

namespace ProductService.Database.DBContext
{
    #region Public Class Definition
    public sealed class DapperContext : IDatabaseContext
    {
        #region Private Field Definition
        private readonly string _defaultEnvironment;
        #endregion

        #region Public Property Definition
        public IConfiguration Configuration { get; set; }
        #endregion

        #region Public Constructor Definition
        public DapperContext()
        {
            // Get the environment this application is running on
            _defaultEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<EFContext>()
                .Build();
        }
        #endregion

        #region Public Method
        public IEnumerable<TResult> Query<TResult>(string sql, object parameters, CommandType commandType)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            using IDbConnection conn = new SqlConnection(Configuration.GetConnectionString(_defaultEnvironment));
            IEnumerable<TResult> result = conn.Query<TResult>(sql, parameters, commandType: commandType);

            timer.Stop();
            
            return result;
        }
        #endregion
    }
    #endregion
}