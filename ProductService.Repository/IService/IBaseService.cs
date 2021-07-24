#region Include Definition
using System;
#endregion

namespace ProductService.Repository.IService
{
    #region Public Interface Definition
    public interface IBaseService : IDisposable
    {
        #region Method Signature
        void SaveChanges();
        #endregion
    }
    #endregion
}