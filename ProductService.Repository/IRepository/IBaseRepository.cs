#region Include Definition
using Microsoft.EntityFrameworkCore;
#endregion

namespace ProductService.Repository.IRepository
{
    #region Public Interface Definition
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region Method Signature
        TEntity Insert(TEntity entity);

        TEntity Save(TEntity entity);

        TEntity Delete(TEntity entity);
        #endregion
    }
    #endregion
}