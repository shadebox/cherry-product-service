#region Include Definition
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Database.Domain;
using ProductService.BusinessLogic.Dtos;
#endregion

namespace ProductService.BusinessLogic.IServices
{
    #region Public Interface Definition
    public interface IProductService
    {
        #region Method Signature
        Task<IList<ProductDto>> GetProductsAsync(int page, int pageSize);

        Task<ProductDto> GetProductAsync(long productID);

        Task<ProductDto> CreateProductAsync(ProductDto productDto);

        Task<ProductDto> UpdateProductAsync(long id, ProductDto productDto);

        Task<ProductDto> DeleteProductAsync(long id);
        #endregion
    }
    #endregion
}