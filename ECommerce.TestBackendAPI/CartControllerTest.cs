using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
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
            // Arrange
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Setup for CartRepository
            //Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
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
            // Arrange
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);

            // Act
            var actionResult = await _cartController.GetCartByUserId(userId);
            var okActionResult = actionResult.Result as OkObjectResult;
            CartDTO data = okActionResult?.Value as CartDTO;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.userId, userId);
            Assert.Equal(data.total, cart.Total);
        }


        [Fact]
        public async void DeleteCart_WithParams_Ok_String()
        {
            // Arrange
            string userId = "05235465-f941-4e00-98bb-5306da1de482";
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            _cartRepository.Setup(_ => _.DeleteCart(cart));
            _cartRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _cartController.DeleteCart(userId);
            var okActionResult = actionResult as OkObjectResult;
            string okData = okActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Equal(okData, "Delete sucessfully");
        }
    }
}
