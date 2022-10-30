using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Data;
using ECommerce.SharedView.DTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerce.TestBackendAPI
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductController _productController;
        private readonly IMapper _mapper;
        public ProductControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _productRepository = new Mock<IProductRepository>();
            _productController = new ProductController(_productRepository.Object, _mapper);
        }
        [Fact]
        public async void GetAllProducts_WithoutParams_Ok_ListProductDTO()
        {
            // Arrange
            _productRepository.Setup(_ => _.GetProducts()).ReturnsAsync(MockData_Product.GetProducts());

            // Act
            var actionResult = await _productController.Test();
            var data = actionResult.Value;

            // Assert
            Assert.IsType<ActionResult<List<ProductDTO>>>(actionResult);
            //Assert.Equal(MockData_Product.GetProducts().Count, actionResult.Value?.Count);
        }

        [Fact]
        public async void GetAllProducts_WithoutParams_Ok_ListProductDTO_()
        {
            // Arrange
            _productRepository.Setup(_ => _.GetProducts()).ReturnsAsync(MockData_Product.GetProducts());

            // Act
            var actionResult = await _productController.TestWithString() as OkObjectResult;
            var data = actionResult.Value;

            // Assert
            Assert.IsType<ActionResult<List<ProductDTO>>>(actionResult);
            //Assert.Equal(MockData_Product.GetProducts().Count, actionResult.Value?.Count);
        }

        //[Fact]
        //public async void GetProductByType_WithParams_Ok_ListProductDTO()
        //{
        //    // Arrange
        //    for (var i = 0; i <= 7; i++)
        //    {
        //        _productRepository.Setup(_ => _.GetProductByType(i)).ReturnsAsync(MockData_Product.GetProductByType(i));
        //    }

        //    // Act
        //    List<ActionResult<List<ShowedProductDTO>>> listActionResult = new List<ActionResult<List<ShowedProductDTO>>>(8);
        //    for (var i = 0; i <= 7; i++)
        //    {
        //        listActionResult.Add(await _productController.GetProductByType(i));
        //    }

        //    // Assert
        //    for (var i = 0; i <= 7; i++)
        //    {
        //        Assert.NotNull(listActionResult[i].Value);
        //        Assert.Equal(MockData_Product.GetProductByType(i).Count, listActionResult[i].Value?.Count);
        //    }
        //}
    }
}
