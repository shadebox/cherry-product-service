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

        // [Fact]
        // public void GetAsyncReturnsEmptyList()
        // {
        //     // Arrange

        //     // Act

        //     // Assert
        // }

        // [Fact]
        // public void GetAsyncReturnsProduct()
        // {            
        //     // Arrange

        //     // Act

        //     // Assert
        // }

        // [Fact]
        // public void GetAsyncReturnsProductNotFound()
        // {
        //     // Arrange

        //     // Act

        //     // Assert
        // }

        // [Fact]
        // public void PostAsyncReturnsBadRequest()
        // {
        //     // Arrange

        //     // Act

        //     // Assert
        // }

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

            return productServiceMock;
        }
        #endregion
    }
    #endregion
}