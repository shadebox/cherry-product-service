#region Include Definition
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
#endregion

namespace ProductService.Repository.IRepository
{
    #region Public Interface Definition
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region Method Signature
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereCondition);
        
        TEntity Insert(TEntity entity);

        TEntity Save(TEntity entity);

        TEntity Delete(TEntity entity);
        #endregion
    }
    #endregion
}