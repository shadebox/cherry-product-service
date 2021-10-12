#region Include Definition
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic.Services;
using System.Threading.Tasks;
using AutoMapper;
using ProductService.Rest.Models.Resources;
using ProductService.BusinessLogic.Dtos;
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
            ProductDto productDto = await _productService.GetProductAsync(productID);
            
            if (productDto == null)
                return NotFound();

            return Ok(new DataResponse<ProductResource>(_mapper.Map<ProductDto, ProductResource>(productDto)));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse(ModelState.GetErrorMessages()));

            ProductDto productDto = await _productService
                .CreateProductAsync(_mapper.Map<ProductResource, ProductDto>(productResource));

            // TODO: Change this to CreatedAt method
            return Ok(_mapper.Map<ProductDto, ProductResource>(productDto));
        }

        [HttpPut("{}")]
        public async Task<IActionResult> PutAsync([FromBody] ProductResource productResource)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
    #endregion
}