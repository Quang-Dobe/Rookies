using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace ECommerce.TestBackendAPI
{
    public class CartDetailControllerTest
    {
        private readonly Mock<ICartDetailRepository> _cartDetailRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<ICartRepository> _cartRepository;
        private CartDetailController _cartDetailController;
        private readonly IMapper _mapper;


        // Initialization
        public CartDetailControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _cartRepository = new Mock<ICartRepository>();
            _cartDetailRepository = new Mock<ICartDetailRepository>();
            _productRepository = new Mock<IProductRepository>();
            _cartDetailController = new CartDetailController(_cartDetailRepository.Object, _productRepository.Object, _cartRepository.Object, _mapper);
        }


        // Test methods
        [Fact]
        public async void GetCartDetail_WithParams_Ok_ShowedCartDetailDTO()
        {
            // Arrange
            int id = 1;
            // Setup for CartDetailRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartDetailRepository.Setup(_ => _.GetCartDetail(id)).ReturnsAsync(MockData_CartDetail.GetListCartDetail(cart).ElementAt(0));

            // Act
            var actionResult = await _cartDetailController.GetCartDetail(id);
            var okActionResult = actionResult.Result as OkObjectResult;
            ShowedCartDetailDTO data = okActionResult.Value as ShowedCartDetailDTO;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Id, id);
        }


        [Fact]
        public async void GetAllCartDetail_WithoutParams_Ok_ListShowedCartDetailDTO()
        {
            // Arrange
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartDetailRepository.Setup(_ => _.GetCartDetail()).ReturnsAsync(MockData_CartDetail.GetListCartDetail(cart));

            // Act
            var actionResult = await _cartDetailController.GetAllCartDetail();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<ShowedCartDetailDTO> data = okActionResult.Value as List<ShowedCartDetailDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, MockData_CartDetail.GetListCartDetail(cart).Count);
        }


        [Fact]
        public async void GetAllCardDetailByCart_WithParams_Ok_ListShowedCartDetailDTO()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            // Setup for CartDetailRepository
            List<CartDetail> cartDetails = MockData_CartDetail.GetListCartDetail(cart);
            _cartDetailRepository.Setup(_ => _.GetCartDetail(cart)).ReturnsAsync(cartDetails);
            // Setup for ProductRepository
            for(int i=0; i<cartDetails.Count; i++)
            {
                _productRepository.Setup(_ => _.GetProductById(cartDetails[i].ProductId)).ReturnsAsync(cartDetails[i].Product);
            }

            // Act
            var actionResult = await _cartDetailController.GetAllCardDetailByCart(userId);
            var okActionResult = actionResult.Result as OkObjectResult;
            List<ShowedCartDetailDTO> data = okActionResult.Value as List<ShowedCartDetailDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, cartDetails.Count);
            for(int i=0; i<cartDetails.Count; i++)
            {
                Assert.Equal(data[i].showedProductDTO.id, cartDetails[i].ProductId);
            }
        }


        [Fact]
        public async void CreateCartDetail_WithParams_Ok_String()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            int productId = 1;
            int number = 1;
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            // Setup for ProductRepository
            // Product here I've already setted productId same as productId above (=1)
            Product product = MockData_CartDetail.GetListCartDetail(cart).ElementAt(0).Product;
            _productRepository.Setup(_ => _.GetProductById(productId)).ReturnsAsync(product);
            // Setup for CartDetailRepository
            // It the same result (Same CartDetail) as above (When getting Product)
            CartDetail cartDetail = MockData_CartDetail.GetListCartDetail(cart).ElementAt(0);
            _cartDetailRepository.Setup(_ => _.GetCartDetail(cart.Id, productId)).ReturnsAsync(cartDetail);
            // Other Setup but I think It's not important
            _cartDetailRepository.Setup(_ => _.Save());
            _cartDetailRepository.Setup(_ => _.CreateCartDetail(cartDetail));

            // Act
            var actionResult = await _cartDetailController.CreateCartDetail(userId, productId, number);
            var okActionResult = actionResult as OkObjectResult;
            string data = okActionResult.Value as string;

            // Assert
            Assert.NotNull(data);
        }


        [Fact]
        public async void UpdateCartDetail_WithParams_Ok_String()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            int productId = 1;
            int number = 1;
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            // Setup for CartDetailRepository
            CartDetail cartDetail = MockData_CartDetail.GetListCartDetail(cart).ElementAt(0);
            _cartDetailRepository.Setup(_ => _.GetCartDetail(cart.Id, productId)).ReturnsAsync(cartDetail);
            // Other Setup but I think It's not important
            _cartDetailRepository.Setup(_ => _.UpdateCartDetail(cartDetail));
            _cartDetailRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _cartDetailController.UpdateCartDetail(userId, productId, number);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
        }

        [Fact]
        public async void DeleteCartDetail_WithParams_Ok_String()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            int productId = 1;
            int number = 1;
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            // Setup for CartDetailRepository
            CartDetail cartDetail = MockData_CartDetail.GetListCartDetail(cart).ElementAt(0);
            _cartDetailRepository.Setup(_ => _.GetCartDetail(cart.Id, productId)).ReturnsAsync(cartDetail);
            // Other Setup but I think It's not important
            _cartDetailRepository.Setup(_ => _.DeleteCartDetail(cartDetail));
            _cartDetailRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _cartDetailController.DeleteCartDetail(userId, productId, number);
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
