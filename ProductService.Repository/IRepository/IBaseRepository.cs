#region Include Definition
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace ProductService.Repository.IRepository
{
    #region Public Interface Definition
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region Method Signature
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereCondition);
        
        Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> whereCondition, int page = 1, int pageSize = 1);

        TEntity Insert(TEntity entity);

        TEntity Save(TEntity entity);

        TEntity Delete(TEntity entity);
        #endregion
    }
    #endregion
}