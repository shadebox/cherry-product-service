#region Include Definition
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Database.Domain;
using ProductService.Repository.IRepository;
using System;
#endregion

namespace ProductService.BusinessLogic.Service
{
    #region Public Class Definition
    public class ProductService : IProductService
    {
        #region Private Field Definition
        private readonly IProductRepository _productRepository;
        #endregion

        #region Public Constructor Definition
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Public Method Definition        
        public async Task<Product> GetProductAsync(int ProductId)
        {
            return await _productRepository.GetSingleAsync(null);
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}