using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.BackendAPI.Service;
using ECommerce.SharedView.DTO.Account;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;


namespace ECommerce.TestBackendAPI
{
    public class AuthControllerTest
    {
        private readonly Mock<IConfiguration> _config;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICartRepository> _cartRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<ITokenManager> _tokenManager;
        private readonly IMapper _mapper;
        private readonly Mock<SignInManager<IdentityUser>> _signInManager;
        private readonly Mock<UserManager<IdentityUser>> _userManager;
        private readonly Mock<RoleManager<IdentityRole>> _roleManager;
        private AuthController _authController;


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
            _tokenManager = new Mock<ITokenManager>();
            
            
            _userManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), 
                null, null, null, null, null, null, null, null);

            _signInManager = new Mock<SignInManager<IdentityUser>>(
                _userManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
                null, null, null, null);

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _roleManager = new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null);


            _authController = new AuthController(_config.Object, _userRepository.Object, _cartRepository.Object, _orderRepository.Object, _tokenManager.Object, _mapper, _signInManager.Object, _userManager.Object, _roleManager.Object);
        }



        [Fact]
        // Test methods
        public async void Login_WithParams_Ok_String()
        {
            // Arrange
            LoginRequestDTO loginRequestModel_1 = new LoginRequestDTO
            {
                UserName = "tdquangcr",
                Password = "123456789z@Z"
            };
            LoginRequestDTO loginRequestModel_2 = new LoginRequestDTO
            {
                UserName = "tdquangcraaaaaaaaaaaa",
                Password = "123456789z@Z"
            };
            // Setup for UserRepository
            _userRepository.Setup(_ => _.GetUserByName(loginRequestModel_1.UserName)).ReturnsAsync(MockData_CartDetail.GetListCart().ElementAt(0).User);
            _userRepository.Setup(_ => _.GetUserByName(loginRequestModel_2.UserName)).ReturnsAsync(MockData_CartDetail.GetListCart().ElementAt(0).User);


            // Act
            var actionResult_1 = await _authController.Login(loginRequestModel_1);
            var okActionResult_1 = actionResult_1 as OkObjectResult;
            var unauthorizedActionResult_1 = actionResult_1 as UnauthorizedObjectResult;
            string okData_1 = okActionResult_1?.Value as string;
            string unauthorizedData_1 = unauthorizedActionResult_1?.Value as string;

            var actionResult_2 = await _authController.Login(loginRequestModel_2);
            var okActionResult_2 = actionResult_2 as OkObjectResult;
            var unauthorizedActionResult_2 = actionResult_2 as UnauthorizedObjectResult;
            string okData_2 = okActionResult_2?.Value as string;
            string unauthorizedData_2 = unauthorizedActionResult_2?.Value as string;

            // Assert
            Assert.NotNull(okData_1);
            Assert.Null(unauthorizedData_1);
            Assert.Null(okData_2);
            Assert.NotNull(unauthorizedData_2);
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
    }
}
