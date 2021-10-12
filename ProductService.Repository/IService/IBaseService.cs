#region Include Definition
using System;
using System.Threading.Tasks;
#endregion

namespace ProductService.Repository.IService
{
    #region Public Interface Definition
    public interface IBaseService : IDisposable
    {
        #region Method Signature
        Task<int> SaveChangesAsync();
        #endregion
    }
    #endregion
}