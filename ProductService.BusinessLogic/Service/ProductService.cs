#region Include Definition
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Database.Domain;
using System;
#endregion

namespace ProductService.BusinessLogic.Service
{
    #region Public Class Definition
    public class ProductService : IProductService
    {
        #region Public Method Definition
        public async Task<IList<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}