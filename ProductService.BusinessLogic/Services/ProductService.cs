#region Include Definition
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Database.Domain;
using ProductService.Repository.IRepository;
using ProductService.Repository.IService;
using ProductService.BusinessLogic.WhereExpression;
using ProductService.BusinessLogic.Dtos;
using System;
using AutoMapper;
#endregion

namespace ProductService.BusinessLogic.Services
{
    #region Public Class Definition
    public class ProductService : IProductService
    {
        #region Private Field Definition
        private readonly IBaseService _baseService;
        private readonly IProductRepository _productRepository;
        private readonly ProductExpression _productExpression;
        private readonly IMapper _mapper;
        #endregion

        #region Public Constructor Definition
        public ProductService(IBaseService baseService, IProductRepository productRepository, ProductExpression productExpression, IMapper mapper)
        {
            _baseService = baseService;
            _productRepository = productRepository;
            _productExpression = productExpression;
            _mapper = mapper;
        }
        #endregion

        #region Public Method Definition        
        public async Task<ProductDto> GetProductAsync(long productID)
        {
            Product product = await _productRepository.GetSingleAsync(_productExpression.GetProduct(new Product { ID = productID }));
            return _mapper.Map<Product, ProductDto>(product);
        }

        // public async Task<IList<ProductDto>> GetProductsAsync(int page, int pageSize)
        // {
        //     IList<Product> products = await _productRepository.GetManyAsync(null, page, pageSize);
        //     return _mapper.Map<IList<Product>, IList<ProductDto>>(products);
        // }
        
        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            try
            {
                Product product = await _productRepository.InsertAsync(_mapper.Map<ProductDto, Product>(productDto));
                await _baseService.SaveChangesAsync();

                return _mapper.Map<Product, ProductDto>(product);
            }
            catch
            {
                
                throw;
            }
        }
        #endregion
    }
    #endregion
}