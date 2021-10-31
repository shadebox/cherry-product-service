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
        }
        #endregion

        #region Public Method Definition
        [Fact]
        public async Task GetAsyncReturnsProducts()
        {
            // Arrange
            IList<ProductDto> productDtos = new List<ProductDto>
            {
                new ProductDto { ProductID = 1, Name = "Blue Queen Bed", Status = 1 }
            };

            Mock<IProductService> mockProductService = GetProductServiceMock(productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            OkObjectResult results = await productController.GetAsync() as OkObjectResult;
            DataResponse<IList<ProductResource>> products = results.Value as DataResponse<IList<ProductResource>>;

            // Assert
            Assert.Equal(200, results.StatusCode);
            Assert.Equal(productDtos.Count(), products.Data.Count());
        }

        [Fact]
        public async Task GetAsyncReturnsProduct()
        {            
            // Arrange
            IList<ProductDto> productDtos = new List<ProductDto>
            {
                new ProductDto { ProductID = 1, Name = "Blue Queen Bed", Status = 1 }
            };

            Mock<IProductService> mockProductService = GetProductServiceMock(productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            OkObjectResult result = await productController.GetAsync(1) as OkObjectResult;
            DataResponse<ProductResource> product = result.Value as DataResponse<ProductResource>;

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(productDtos.First().ProductID, product.Data.ProductID);
        }

        [Fact]
        public async Task GetAsyncReturnsProductNotFound()
        {
            // Arrange
            IList<ProductDto> productDtos = new List<ProductDto>
            {
                new ProductDto { ProductID = 1, Name = "Blue Queen Bed", Status = 1 }
            };

            Mock<IProductService> mockProductService = GetProductServiceMock(productDtos);
            ProductController productController = new ProductController(mockProductService.Object, _mapper);

            // Act
            NotFoundResult result = await productController.GetAsync(10) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void PostAsyncReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        // [Fact]
        // public void PostAsyncReturnsSuccess()
        // {
        //     // Arrange

        //     // Act

        //     // Assert
        // }

        // [Fact]
        // public void DeleteAsyncReturnsProductNotFound()
        // {
        //     // Arrange

        //     // Act

        //     // Assert
        // }        

        // [Fact]
        // public void DeleteAsyncReturnsSuccess()
        // {
        //     // Arrange

        //     // Act

        //     // Assert
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

            return productServiceMock;
        }
        #endregion
    }
    #endregion
}