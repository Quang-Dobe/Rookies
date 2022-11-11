using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerce.TestBackendAPI
{
    public class DashboardControllerTest
    {
        private readonly Mock<IOrderDetailRepository> _orderDetailRepository;
        private readonly Mock<ICartDetailRepository> _cartDetailRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly DashboardController _dashboardController;


        public DashboardControllerTest()
        {
            _orderDetailRepository = new Mock<IOrderDetailRepository>();
            _cartDetailRepository = new Mock<ICartDetailRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _dashboardController = new DashboardController(_orderDetailRepository.Object, _categoryRepository.Object, _cartDetailRepository.Object);
        }


        [Fact]
        public async void OrderByOrderAndCart_WithoutParams_Ok_String_ListTotalOrderInCategoryDTO()
        {
            List<Category> categories = MockData_Category.GetAllCategory();
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategories()).ReturnsAsync(categories);
            foreach (Category category in categories)
            {
                _orderDetailRepository.Setup(_ => _.GetTotalOrderByCategory(category.Id)).ReturnsAsync(1);
                _cartDetailRepository.Setup(_ => _.GetTotalCartDetailByCategory(category.Id)).ReturnsAsync(1);
            }

            // Act
            var actionResult = await _dashboardController.OrderByOrderAndCart();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<TotalOrderInCategoryDTO> data = okActionResult.Value as List<TotalOrderInCategoryDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, categories.Count);
        }


        [Fact]
        public async void TotalInThreeDays_WithoutParams_Ok_ListInt()
        {
            // Arrange
            _orderDetailRepository.Setup(_ => _.GetTotalByDate(DateTime.Today.AddDays(-1))).ReturnsAsync(100);
            _orderDetailRepository.Setup(_ => _.GetTotalByDate(DateTime.Today.AddDays(-2))).ReturnsAsync(200);
            _orderDetailRepository.Setup(_ => _.GetTotalByDate(DateTime.Today.AddDays(-3))).ReturnsAsync(300);

            // Act
            var actionResult = await _dashboardController.TotalInThreeDays();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<int> data = okActionResult.Value as List<int>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, 3);
        }


        [Fact]
        public async void OrderInThreeDays_WithoutParams_Ok_ListInt()
        {
            // Arrange
            _orderDetailRepository.Setup(_ => _.GetTotalOrderByDate(DateTime.Today.AddDays(-1))).ReturnsAsync(1);
            _orderDetailRepository.Setup(_ => _.GetTotalOrderByDate(DateTime.Today.AddDays(-2))).ReturnsAsync(2);
            _orderDetailRepository.Setup(_ => _.GetTotalOrderByDate(DateTime.Today.AddDays(-3))).ReturnsAsync(3);

            // Act
            var actionResult = await _dashboardController.OrderInThreeDays();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<int> data = okActionResult.Value as List<int>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, 3);
        }


        [Fact]
        public async void TotalOfEachCategory_WithoutParams_Ok_ListTotalInCategoryDTO()
        {
            List<Category> categories = MockData_Category.GetAllCategory();
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategories()).ReturnsAsync(categories);
            foreach(Category category in categories)
            {
                _orderDetailRepository.Setup(_ => _.GetTotalByCategory(category.Id)).ReturnsAsync(100);
            }

            // Act
            var actionResult = await _dashboardController.TotalOfEachCategory();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<TotalInCategoryDTO> data = okActionResult?.Value as List<TotalInCategoryDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, categories.Count);
            foreach(TotalInCategoryDTO totalInCategoryDTO in data)
            {
                Assert.Equal(totalInCategoryDTO.total, (double)1.0 / categories.Count);
            }

        }
    }
}
