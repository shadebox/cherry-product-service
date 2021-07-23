#region Include Definition
using System.Collections.Generic;
using System.Data;
#endregion

namespace ProductService.Database.DBContext
{
    #region Public Interface Definition
    public interface IDatabaseContext
    {
        IEnumerable<TResult> Query<TResult>(string sql, object parameters, CommandType commandType);
    }
    #endregion
}