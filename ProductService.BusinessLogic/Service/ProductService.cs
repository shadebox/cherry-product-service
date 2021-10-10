#region Include Definition
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Database.Domain;
using ProductService.Repository.IRepository;
using ProductService.BusinessLogic.WhereExpression;
using System;
#endregion

namespace ProductService.BusinessLogic.Service
{
    #region Public Class Definition
    public class ProductService : IProductService
    {
        #region Private Field Definition
        private readonly IProductRepository _productRepository;
        private readonly ProductExpression _productExpression;
        #endregion

        #region Public Constructor Definition
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _productExpression = new ProductExpression();
        }
        #endregion

        #region Public Method Definition        
        public async Task<Product> GetProductAsync(long productID)
        {
            return await _productRepository.GetSingleAsync(_productExpression.GetProduct(new Product { ID = productID }));
        }

        public async Task<IList<Product>> GetProductsAsync(int page, int pageSize)
        {
            return await _productRepository.GetManyAsync(null, page, pageSize);
        }
        #endregion
    }
    #endregion
}