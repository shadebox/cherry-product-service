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
        public async Task<IList<ProductDto>> GetProductsAsync(int page, int pageSize)
        {
            IList<Product> products = await _productRepository.GetManyAsync(null, page, pageSize);
            return _mapper.Map<IList<Product>, IList<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductAsync(long id)
        {
            Product product = await _productRepository
                .GetSingleAsync(_productExpression.GetProduct(new Product { ID = id }));
            return _mapper.Map<Product, ProductDto>(product);
        }
        
        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            try
            {
                Product newProduct = await _productRepository.InsertAsync(_mapper.Map<ProductDto, Product>(productDto));
                await _baseService.SaveChangesAsync();

                return _mapper.Map<Product, ProductDto>(newProduct);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDto> UpdateProductAsync(long id, ProductDto productDto)
        {
            Product product = await _productRepository
                .GetSingleAsync(_productExpression.GetProduct(new Product { ID = id }));

            if (product == null)
                return null;

            try
            {
                _mapper.Map<ProductDto, Product>(productDto, product);
                product.ID = id;

                _productRepository.Save(product);
                int value = await _baseService.SaveChangesAsync();

                return _mapper.Map<Product, ProductDto>(product);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteProductAsync(ProductDto productDto)
        {
            try
            {
                Product deleteProduct = _mapper.Map<ProductDto, Product>(productDto);
                _productRepository.Delete(deleteProduct);
                int value = await _baseService.SaveChangesAsync();
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