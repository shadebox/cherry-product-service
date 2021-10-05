#region Include Definition
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
#endregion

namespace ProductService.Rest.Controllers
{
    #region Public Class Definition
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        #region Private Field Definition
        private readonly IProductService _productService;
        #endregion

        #region Public Constructor Definition
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Public Method Definition
        [HttpGet]
        public async Task<IEnumerable<string>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    #endregion
}