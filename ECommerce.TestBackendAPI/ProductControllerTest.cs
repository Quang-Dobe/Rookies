using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Data;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace ECommerce.TestBackendAPI
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductController _productController;
        private readonly IMapper _mapper;


        // Intialization
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


        // Test methods

        //[Fact]
        //public async void GetAllProducts_WithoutParams_Ok_ListProductDTO_()
        //{
        //    // Arrange
        //    _productRepository.Setup(_ => _.GetProducts()).ReturnsAsync(MockData_Product.GetProducts());

        //    // Act
        //    var actionResult = await _productController.TestLogicForWritingTest();
        //    List<ProductDTO> data = (List<ProductDTO>)actionResult.Value;

        //    // Assert
        //    Assert.NotNull(data);
        //    Assert.Equal(MockData_Product.GetProducts().Count, data.Count);
        //}

        [Fact]
        public async void GetAllProducts_WithoutParams_Ok_ListProductDTO()
        {
            // Arrange
            _productRepository.Setup(_ => _.GetProducts()).ReturnsAsync(MockData_Product.GetProducts());

            // Act
            var actionResult = await _productController.GetAllProducts();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<AllProductDTO> data = (List<AllProductDTO>)okActionResult.Value;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(MockData_Product.GetProducts().Count, data.Count);
        }


        [Fact]
        public async void GetProductByType_WithParams_Ok_ListProductDTO()
        {
            // Arrange
            for (var i = 0; i <= 7; i++)
            {
                _productRepository.Setup(_ => _.GetProductByType(i)).ReturnsAsync(MockData_Product.GetProductByType(i));
            }

            // Act
            List<List<ShowedProductDTO>> listActionResult = new List<List<ShowedProductDTO>>(8);
            for (var i = 0; i <= 7; i++)
            {
                var actionResult = await _productController.GetProductByType(i);
                var okActionResult = actionResult.Result as OkObjectResult;
                List<ShowedProductDTO> data = (List<ShowedProductDTO>)okActionResult.Value;
                listActionResult.Add(data);
            }

            // Assert
            for (var i = 0; i <= 7; i++)
            {
                Assert.NotNull(listActionResult[i].Count);
                Assert.Equal(MockData_Product.GetProductByType(i).Count, listActionResult[i].Count);
            }
        }


        [Fact]
        public async void GetProductByID_WithParams_Ok_ProductDTO()
        {
            // Arrange
            detailProductDTO detailProductDTO = MockData_Product.CreateDetailProductDTO().ElementAt(0);
            _productRepository.Setup(_ => _.GetProductDetailById(0)).ReturnsAsync(detailProductDTO);

            // Act
            var actionResult = await _productController.GetProductByID(0);
            var okActionResult = actionResult.Result as OkObjectResult;
            detailProductDTO data = okActionResult.Value as detailProductDTO;

            // Assert
            var expectedObject = JsonConvert.SerializeObject(detailProductDTO);
            var actualObject = JsonConvert.SerializeObject(data);
            Assert.NotNull(data);
            Assert.Equal(actualObject, expectedObject);
        }


        [Fact]
        public async void CreateNewProduct_WithParams_Ok_String()
        {
            // Arrange
            AllProductDTO allProductDTO = MockData_Product.CreateAllProductDTO().ElementAt(0);

            // Act
            var actionResult = await _productController.CreateNewProduct(allProductDTO);
            var okActionResult = actionResult as OkObjectResult;
            var data = okActionResult.Value;


            // Assert
            Assert.NotNull(data != null);
        }
    }
}
