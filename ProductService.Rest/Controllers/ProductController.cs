#region Include Definition
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AutoMapper;
using ProductService.Rest.Models.Resources;
using ProductService.Database.Domain;
#endregion

namespace ProductService.Rest.Controllers
{
    #region Public Class Definition
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        #region Private Field Definition
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        #endregion

        #region Public Constructor Definition
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        #endregion

        #region Public Method Definition
        [HttpGet]
        public async Task<ProductResource> GetProductAsync()
        {
            var product = await _productService.GetProductAsync(1);
            
            return _mapper.Map<Product, ProductResource>(product);
        }
        #endregion
    }
    #endregion
}