#region Include Definition
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Database.Domain;
#endregion

namespace ProductService.BusinessLogic.Service
{
    #region Public Interface Definition
    public interface IProductService
    {
        #region Method Signature
        Task<IList<Product>> GetProductsAsync();
        #endregion
    }
    #endregion
}