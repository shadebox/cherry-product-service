#region Include Definition
using Microsoft.AspNetCore.Mvc;
using ProductService.BusinessLogic.IServices;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IList<ProductDto> productDtos = await _productService.GetProductsAsync(1, 10);

            return Ok(new DataResponse<IList<ProductResource>>(
                _mapper.Map<IList<ProductDto>, IList<ProductResource>>(productDtos)));
        }

        [HttpGet("{id}")]
        //[ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(long id)
        {
            ProductDto productDto = await _productService.GetProductAsync(id);
            
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

            return CreatedAtAction(nameof(GetAsync), new { id = productDto.ProductID }, 
                new DataResponse<ProductResource>(_mapper.Map<ProductDto, ProductResource>(productDto)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse(ModelState.GetErrorMessages()));
            
            ProductDto productDto = await _productService
                .UpdateProductAsync(id, _mapper.Map<ProductResource, ProductDto>(productResource));

            if (productDto == null)
                return NotFound();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            ProductDto productDto = await _productService.DeleteProductAsync(id);

            if (productDto == null)
                return NotFound();            

            return NoContent();
        }
        #endregion
    }
    #endregion
}