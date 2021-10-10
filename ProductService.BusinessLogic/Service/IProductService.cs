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
        Task<Product> GetProductAsync(long productID);

        Task<IList<Product>> GetProductsAsync(int page, int pageSize);
        #endregion
    }
    #endregion
}