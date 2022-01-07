#region Include Definition
using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.BusinessLogic.IServices;
using ProductService.BusinessLogic.Dtos;
using ProductService.Rest.Profiles;
using ProductService.Rest.Controllers;
using ProductService.Rest.Models.Resources;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace ProducrService.Test.Rest.Controllers
{
    #region Public Class Definition
    public class ProductControllerTest
    {        
        #region Private Field Definition
        private static IMapper _mapper;
        private readonly IList<ProductDto> _productDtos;
        private readonly ProductResource _productResource;
        #endregion

        #region Public Constructor Definition
        public ProductControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mappingConfig =>
                {
                    mappingConfig.AddProfile(new ModelToDtoProfile());
                });

                _mapper = mappingConfig.CreateMapper();
            }

            _productDtos = new List<ProductDto>
            {
                new ProductDto { ProductID = 1, Name = "Blue Queen Bed", Status = 1 }
            };

            
            _productResource = new ProductResource { Name = "Blue Queen Bed", Status = 1 };
        }
        #endregion

        #region Public Method Definition
        [Fact]
        public async Task GetAsyncReturnsOkResult()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            OkObjectResult okObjectResult = await productController.GetAsync() as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResult);            
        }

        [Fact]
        public async Task GetAsyncReturnsProducts()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);
            
            // Act
            OkObjectResult okObjectResult = await productController.GetAsync() as OkObjectResult;

            // Assert
            DataResponse<IList<ProductResource>> dataResponse = Assert.IsType<DataResponse<IList<ProductResource>>>(okObjectResult.Value);
            Assert.Equal(1, dataResponse.Data.Count);
        }        

        [Fact]
        public async Task GetAsyncWithIdReturnsNotFoundResult()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            NotFoundResult result = await productController.GetAsync(0) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAsyncWithIdReturnsOkResult()
        {            
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            OkObjectResult result = await productController.GetAsync(1) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAsyncWithIdReturnsProduct()
        {            
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            OkObjectResult result = await productController.GetAsync(1) as OkObjectResult;
            DataResponse<ProductResource> product = result.Value as DataResponse<ProductResource>;

            // Assert
            var item = Assert.IsType<DataResponse<ProductResource>>(result.Value);
            Assert.Equal(_productDtos.First().ProductID, item.Data.ProductID);
        }

        [Fact]
        public async Task PostAsyncReturnsBadRequest()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);
            
            productController.ModelState.AddModelError("Name", "Product Name Required.");

            // Act
            BadRequestObjectResult result = await productController.PostAsync(_productResource) as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostAsyncReturnsCreatedResponse()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            CreatedAtActionResult result = await productController.PostAsync(_productResource) as CreatedAtActionResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostAsyncReturnsCreatedProduct()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            CreatedAtActionResult result = await productController.PostAsync(_productResource) as CreatedAtActionResult;
            DataResponse<ProductResource> product = result.Value as DataResponse<ProductResource>;

            // Assert
            var item = Assert.IsType<DataResponse<ProductResource>>(result.Value);
            Assert.Equal(_productResource.Name, item.Data.Name);
        }

        [Fact]
        public async Task DeleteAsyncReturnsNotFoundResult()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            NotFoundResult result = await productController.DeleteAsync(0) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }   

        [Fact]
        public async Task DeleteAsyncReturnsNoContentResult()
        {
            // Arrange
            Mock<IProductService> mockProductService = GetProductServiceMock(_productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            NoContentResult result = await productController.DeleteAsync(1) as NoContentResult;
            
            // Assert
            Assert.IsType<NoContentResult>(result);
        }




        // [Fact]
        // public async Task GetAsyncReturnsProducts()
        // {
        //     // Arrange
        //     IList<ProductDto> productDtos = new List<ProductDto>
        //     {
        //         new ProductDto { ProductID = 1, Name = "Blue Queen Bed", Status = 1 }
        //     };

        //     Mock<IProductService> mockProductService = GetProductServiceMock(productDtos);
        //     ProductController productController = new ProductController(mockProductService.Object, _mapper);

        //     // Act
        //     OkObjectResult results = await productController.GetAsync() as OkObjectResult;
        //     DataResponse<IList<ProductResource>> products = results.Value as DataResponse<IList<ProductResource>>;

        //     // Assert
        //     Assert.Equal(200, results.StatusCode);
        //     Assert.Equal(productDtos.Count(), products.Data.Count());
        // }
        #endregion

        #region Private Method Definition
        private Mock<IProductService> GetProductServiceMock(IList<ProductDto> products)
        {
            Mock<IProductService> productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Func<int, int, IList<ProductDto>>((y, z) => products.ToList()));

            productServiceMock.Setup(x => x.GetProductAsync(It.IsAny<long>()))
                .ReturnsAsync(new Func<long, ProductDto>((y) => products.Where(z => z.ProductID == y).SingleOrDefault()));

            productServiceMock.Setup(x => x.CreateProductAsync(It.IsAny<ProductDto>()))
                .ReturnsAsync(new Func<ProductDto, ProductDto>((y) => 
                    { 
                        y.ProductID = 1; 
                        return y; 
                    }
                ));

            productServiceMock.Setup(x => x.DeleteProductAsync(It.IsAny<long>()))
                .ReturnsAsync(new Func<long, ProductDto>((y) => 
                    { 
                        return products.Where(z => z.ProductID == y).SingleOrDefault(); 
                    }
                ));

            return productServiceMock;
        }
        #endregion
    }
    #endregion
}