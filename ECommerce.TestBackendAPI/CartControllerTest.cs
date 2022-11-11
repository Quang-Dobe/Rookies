using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace ECommerce.TestBackendAPI
{
    public class CartControllerTest
    {
        private readonly Mock<ICartRepository> _cartRepository;
        private readonly Mock<ICartDetailRepository> _cartDetailRepository;
        private CartController _cartController;
        private readonly IMapper _mapper;


        // Initialization
        public CartControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _cartRepository = new Mock<ICartRepository>();
            _cartDetailRepository = new Mock<ICartDetailRepository>();
            _cartController = new CartController(_cartRepository.Object, _cartDetailRepository.Object, _mapper);
        }


        // Test methods
        [Fact]
        public async void CreateCart_WithParams_Ok_String()
        {
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Arrange
            // Setup for CartRepository
            _cartRepository.Setup(_ => _.CreateCart(userId));
            _cartRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _cartController.CreateCart(userId);
            var okActionResult = actionResult as OkObjectResult;
            string data = okActionResult?.Value as string;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data, "Create sucessfully");
        }


        [Fact]
        public async void GetCartByUserId_WithParams_Ok_CartDTO()
        {
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Arrange
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);

            // Act
            var actionResult = await _cartController.GetCartByUserId(userId);

            var okActionResult = actionResult.Result as OkObjectResult;
            var badrequestActionResult = actionResult.Result as BadRequestObjectResult;

            CartDTO okData = okActionResult?.Value as CartDTO;
            var badrequestData = badrequestActionResult?.Value;

            // Assert
            Assert.NotNull(okData);
            Assert.Equal(okData.userId, userId);
            Assert.Equal(okData.total, cart.Total);

            Assert.Null(badrequestData);
        }


        [Fact]
        public async void GetCartByUserId_WithParams_BadRequest_CartDTO()
        {
            string userId = "Invalid";
            // Arrange
            // Setup for CartRepository
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync((Cart)null);

            // Act
            var actionResult = await _cartController.GetCartByUserId(userId);

            var okActionResult = actionResult.Result as OkObjectResult;
            var badrequestActionResult = actionResult.Result as BadRequestObjectResult;

            CartDTO okData = okActionResult?.Value as CartDTO;
            var badrequestData = badrequestActionResult?.Value;

            // Assert
            Assert.Null(okData);

            Assert.NotNull(badrequestData);
            Assert.Equal(badrequestData, "Invalid userId");
        }


        [Fact]
        public async void DeleteCart_WithParams_Ok_String()
        {
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Arrange
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            _cartRepository.Setup(_ => _.DeleteCart(cart));
            _cartRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _cartController.DeleteCart(userId);

            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;

            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Equal(okData, "Delete sucessfully");

            Assert.Null(badrequestData);
        }


        [Fact]
        public async void DeleteCart_WithParams_BadRequest_String()
        {
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Arrange
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync((Cart)null);
            _cartRepository.Setup(_ => _.DeleteCart(cart));
            _cartRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _cartController.DeleteCart(userId);

            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;

            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.Null(okData);

            Assert.NotNull(badrequestData);
            Assert.Equal(badrequestData, "Invalid userId");
        }
    }
}
