using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Data;
using ECommerce.Data.Model;
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
        private readonly Mock<ICategoryRepository> _categoryRepository;
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
            _categoryRepository = new Mock<ICategoryRepository>();
            _productController = new ProductController(_categoryRepository.Object, _productRepository.Object, _mapper);
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
            List<Product> products = MockData_Product.GetProducts();
            _productRepository.Setup(_ => _.GetProducts()).ReturnsAsync(products);

            // Act
            var actionResult = await _productController.GetAllProducts();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<AllProductDTO> data = (List<AllProductDTO>)okActionResult.Value;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(MockData_Product.GetProducts().Count, data.Count);
            for (int i=0; i<data.Count; i++)
            {
                Assert.Equal(data.ElementAt(i).id, products.ElementAt(i).Id);
            }
        }


        [Fact]
        public async void GetProductByType_WithParams_Ok_ListProductDTO()
        {
            List<Category> categories = MockData_Category.GetAllCategory();
            // Arrange
            for (var i = 0; i <= 7; i++)
            {
                _categoryRepository.Setup(_ => _.GetCategory(i)).ReturnsAsync(categories.ElementAt(i));
                _productRepository.Setup(_ => _.GetProductByType(i)).ReturnsAsync(MockData_Product.GetProductByType(i));
            }

            // Act
            List<List<ShowedProductDTO>> listActionResult = new List<List<ShowedProductDTO>>(8);
            for (var i = 0; i <= 7; i++)
            {
                var actionResult = await _productController.GetProductByType(i);
                var okActionResult = actionResult.Result as OkObjectResult;
                List<ShowedProductDTO> data = okActionResult.Value as List<ShowedProductDTO>;
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
            Product product = MockData_Product.GetProducts().ElementAt(0);
            AllProductDTO allProductDTO = MockData_Product.CreateAllProductDTO().ElementAt(0);
            // Setup for Category Repository
            _categoryRepository.Setup(_ => _.GetCategory(allProductDTO.CategoryId)).ReturnsAsync(MockData_Category.GetAllCategory().ElementAt(0));
            // Setup for Product Repository
            _productRepository.Setup(_ => _.CreateProduct(product));
            _productRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _productController.CreateNewProduct(allProductDTO);
            var okActionResult = actionResult as OkObjectResult;
            string data = okActionResult.Value as string;


            // Assert
            Assert.NotNull(data);
            Assert.Equal(data, "Success: Create successfully!");
        }


        [Fact]
        public async void UpdateProduct_WithParams_Ok_String()
        {
            Product product = MockData_Product.GetProducts().ElementAt(0);
            // Arrange
            _productRepository.Setup(_ => _.GetProductById(product.Id)).ReturnsAsync(product);
            _productRepository.Setup(_ => _.UpdateProduct(product));
            _productRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _productController.UpdateProduct(product.Id, MockData_Product.CreateAllProductDTO().ElementAt(0));
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
        }


        [Fact]
        public async void UpdateMultiProduct_WithParams_Ok_String()
        {
            List<AllProductDTO> allProductDTOs = MockData_Product.CreateAllProductDTO();
            List<Product> products = MockData_Product.GetProducts();
            // Arrange
            for (int i=0; i<allProductDTOs.Count; i++)
            {
                _productRepository.Setup(_ => _.GetProductById(allProductDTOs[i].id)).ReturnsAsync(products[i]);
            }

            // Act
            var actionResult = await _productController.UpdateMultiProduct(allProductDTOs);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
        }


        [Fact]
        public async void DeleteProduct_WithParams_Ok_String()
        {
            Product product = MockData_Product.GetProducts().ElementAt(0);
            // Arrange
            _productRepository.Setup(_ => _.GetProductById(product.Id)).ReturnsAsync(product);
            _productRepository.Setup(_ => _.DeleteProduct(product.Id));
            _productRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _productController.DeleteProduct(product.Id);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
        }


        [Fact]
        public async void DeleteMultiProduct_WithParams_Ok_String()
        {
            List<int> ids = new List<int> { 1, 2, 3 };
            // Arrange
            foreach (int id in ids)
            {
                Product product = MockData_Product.GetProducts().ElementAt(id-1);
            
                _productRepository.Setup(_ => _.GetProductById(product.Id)).ReturnsAsync(product);
                _productRepository.Setup(_ => _.DeleteProduct(product.Id));
                _productRepository.Setup(_ => _.Save());
            }

            // Act
            var actionResult = await _productController.DeleteMultiProduct(ids);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
        }
    }
}
