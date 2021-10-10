#region Include Definition
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic.Service;
using System.Threading.Tasks;
using AutoMapper;
using ProductService.Rest.Models.Resources;
using ProductService.Rest.Models.Bindings;
using ProductService.Database.Domain;
using ProductService.Rest.Extensions;
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
        [HttpGet("{productID}")]
        public async Task<IActionResult> GetProductAsync(long productID)
        {
            Product product = await _productService.GetProductAsync(productID);
            
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<Product, ProductResource>(product));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductBinding productBinding)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            Product product = _mapper.Map<SaveProductBinding, Product>(productBinding);

            throw new System.NotImplementedException();
        }
        #endregion
    }
    #endregion
}