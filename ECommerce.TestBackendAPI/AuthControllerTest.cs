using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.SharedView.DTO.Account;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;


namespace ECommerce.TestBackendAPI
{
    public class AuthControllerTest
    {
        private readonly Mock<IConfiguration> _config;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICartRepository> _cartRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private AuthController _authController;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;


        // Initialization
        public AuthControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _config = new Mock<IConfiguration>();
            _userRepository = new Mock<IUserRepository>();
            _cartRepository = new Mock<ICartRepository>();
            _orderRepository = new Mock<IOrderRepository>();
            _authController = new AuthController(_config.Object, _userRepository.Object, _cartRepository.Object, _orderRepository.Object, _mapper, _signInManager, _userManager);
        }


        [Fact]
        // Test methods
        public async void Login_WithParams_Ok_String()
        {
            // Arrange
            LoginRequestDTO loginRequestModel = new LoginRequestDTO
            {
                UserName = "tdquangcr",
                Password = "123456789z@Z"
            };
            // Setup for UserRepository
            _userRepository.Setup(_ => _.GetUserByEmail(loginRequestModel.UserName)).ReturnsAsync(MockData_CartDetail.GetListCart().ElementAt(0).User);

            // Act
            var actionResult = await _authController.Login(loginRequestModel);
            var okActionResult = actionResult as OkObjectResult;
            var unauthorizedActionResult = actionResult as UnauthorizedObjectResult;
            string okData = okActionResult?.Value as string;
            string unauthorizedData = unauthorizedActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(unauthorizedData);
        }


        [Fact]
        public async void Register_WithParams_Ok_String()
        {
            // Arrange
            RegisterRequestDTO registerRequestModel = new RegisterRequestDTO
            {
                Email = "tdquangcr@gmail.com",
                UserName = "tdquangcr",
                Password = "123456789z@Z",
                ConfirmPassword = "123456789z@Z",
                PhoneNumber = "0356131798"
            };
            IdentityUser user = MockData_CartDetail.GetListCart().ElementAt(0).User;
            // Setup for CartRepository and OrderRepository
            _cartRepository.Setup(_ => _.CreateCart(user.Id));
            _orderRepository.Setup(_ => _.CreateOrder(user.Id));
            _cartRepository.Setup(_ => _.Save());
            _orderRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _authController.Register(registerRequestModel);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult?.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
            Assert.Equal(okData, "Create sucessfully!");
        }


        [Fact]
        public async void GetAllUser_WithoutParams_Ok_ListAllUserDTO()
        {
            // Arrange
            List<IdentityUser> userList = MockData_User.GetUsers();
            _userRepository.Setup(_ => _.GetUsers()).ReturnsAsync(userList);

            // Act
            var actionResult = await _authController.GetAllUser();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<AllUserDTO> data = okActionResult.Value as List<AllUserDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, userList.Count);
        }
    }
}
