#region Include Definition
using System.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using Dapper;
#endregion

namespace ProductService.Database.DBContext
{
    #region Public Class Definition
    public sealed class DapperContext : IDatabaseContext
    {
        #region Private Field Definition
        private readonly string connectionString;
        #endregion

        #region Public Constructor Definition
        public DapperContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #endregion

        #region Public Method
        public IEnumerable<TResult> Query<TResult>(string sql, object parameters, CommandType commandType)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            using IDbConnection conn = new SqlConnection(connectionString);
            IEnumerable<TResult> result = conn.Query<TResult>(sql, parameters, commandType: commandType);

            timer.Stop();
            
            return result;
        }
        #endregion
    }
    #endregion
}