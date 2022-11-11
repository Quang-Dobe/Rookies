using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerce.TestBackendAPI
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly CategoryController _categoryController;
        private readonly IMapper _mapper;


        public CategoryControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryController = new CategoryController(_categoryRepository.Object, _mapper);
        }


        [Fact]
        public async void GetCategory_WithParams_Ok_AllCategoryDTO()
        {
            int Id_1 = 0;
            int Id_2 = 100;
            Category category = MockData_Category.GetAllCategory().ElementAt(0);
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategory(Id_1)).ReturnsAsync(category);
            _categoryRepository.Setup(_ => _.GetCategory(Id_2)).ReturnsAsync((Category)null) ;

            // Act
            var actionResult_1 = await _categoryController.GetCategory(Id_1);
            var okActionResult_1 = actionResult_1.Result as OkObjectResult;
            var badrequestActionResult_1 = actionResult_1.Result as BadRequestObjectResult;
            AllCategoryDTO okData_1 = okActionResult_1?.Value as AllCategoryDTO;
            AllCategoryDTO badrequestData_1 = badrequestActionResult_1?.Value as AllCategoryDTO;

            var actionResult_2 = await _categoryController.GetCategory(Id_2);
            var okActionResult_2 = actionResult_2.Result as OkObjectResult;
            var badrequestActionResult_2 = actionResult_2.Result as BadRequestObjectResult;
            string okData_2 = okActionResult_2?.Value as string;
            string badrequestData_2 = badrequestActionResult_2?.Value as string;

            // Assert
            Assert.NotNull(okData_1);
            Assert.Null(badrequestData_1);
            Assert.Null(okData_2);
            Assert.NotNull(badrequestData_2);
        }


        [Fact]
        public async void GetAllCategory_WithoutParams_Ok_ListAllCategoryDTO()
        {
            List<Category> categories = MockData_Category.GetAllCategory();
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategories()).ReturnsAsync(categories);

            // Act
            var actionResult = await _categoryController.GetAllCategory();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<AllCategoryDTO> data = okActionResult.Value as List<AllCategoryDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, categories.Count);
            for (int i=0; i<data.Count; i++)
            {
                Assert.Equal(data[i].id, categories[i].Id);
            }
        }


        [Fact]
        public async void GetAllCategoryAdmin_WithoutParams_Ok_ListAllCategoryDTO()
        {
            List<Category> categories = MockData_Category.GetAllCategory();
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategories()).ReturnsAsync(categories);

            // Act
            var actionResult = await _categoryController.GetAllCategoryAdmin();
            var okActionResult = actionResult.Result as OkObjectResult;
            List<AllCategoryDTO> data = okActionResult.Value as List<AllCategoryDTO>;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Count, categories.Count);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i].id, categories[i].Id);
            }
        }


        [Fact]
        public async void CreateCategory_WithParams_Ok_String()
        {
            AllCategoryDTO allCategoryDTO = new AllCategoryDTO { id = 8, name="Hola", description = "Halo"};
            // Arrange
            _categoryRepository.Setup(_ => _.CreateCategory(allCategoryDTO.name, allCategoryDTO.description));
            _categoryRepository.Setup(_ => _.Save());

            // Act
            var actionResult = await _categoryController.CreateCategory(allCategoryDTO);
            var okActionResult = actionResult as OkObjectResult;
            string data = okActionResult.Value as string;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data, "Create Category Sucessfully!");
        }


        [Fact]
        public async void UpdateCategory_WithParams_Ok_String()
        {
            Category category = MockData_Category.GetAllCategory().ElementAt(0);
            AllCategoryDTO allCategoryDTO_1 = MockData_Category.GetAllCategoryDTO().ElementAt(0);
            AllCategoryDTO allCategoryDTO_2 = new AllCategoryDTO { id = 100, name = "Hola", description = "Halo" };
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategory(category.Id)).ReturnsAsync(category);
            _categoryRepository.Setup(_ => _.GetCategory(100)).ReturnsAsync((Category)null);

            // Act
            var actionResult_1 = await _categoryController.UpdateCategory_(allCategoryDTO_1);
            var okActionResult_1 = actionResult_1 as OkObjectResult;
            var badrequestActionResult_1 = actionResult_1 as BadRequestObjectResult;
            string okData_1 = okActionResult_1?.Value as string;
            string badrequestData_1 = badrequestActionResult_1?.Value as string;

            var actionResult_2 = await _categoryController.UpdateCategory_(allCategoryDTO_2);
            var okActionResult_2 = actionResult_2 as OkObjectResult;
            var badrequestActionResult_2 = actionResult_2 as BadRequestObjectResult;
            string okData_2 = okActionResult_2?.Value as string;
            string badrequestData_2 = badrequestActionResult_2?.Value as string;

            // Assert
            Assert.NotNull(okData_1);
            Assert.Null(badrequestData_1);
            Assert.Null(okData_2);
            Assert.NotNull(badrequestData_2);
        }


        [Fact]
        public async void DeleteCategory_WithParams_Ok_String()
        {
            Category category = MockData_Category.GetAllCategory().ElementAt(0);
            AllCategoryDTO allCategoryDTO_1 = MockData_Category.GetAllCategoryDTO().ElementAt(0);
            AllCategoryDTO allCategoryDTO_2 = new AllCategoryDTO { id = 100, name = "Hola", description = "Halo" };
            // Arrange
            _categoryRepository.Setup(_ => _.GetCategory(category.Id)).ReturnsAsync(category);
            _categoryRepository.Setup(_ => _.GetCategory(100)).ReturnsAsync((Category)null);

            // Act
            var actionResult_1 = await _categoryController.DeleteCategory(allCategoryDTO_1.id);
            var okActionResult_1 = actionResult_1 as OkObjectResult;
            var badrequestActionResult_1 = actionResult_1 as BadRequestObjectResult;
            string okData_1 = okActionResult_1?.Value as string;
            string badrequestData_1 = badrequestActionResult_1?.Value as string;

            var actionResult_2 = await _categoryController.DeleteCategory(allCategoryDTO_2.id);
            var okActionResult_2 = actionResult_2 as OkObjectResult;
            var badrequestActionResult_2 = actionResult_2 as BadRequestObjectResult;
            string okData_2 = okActionResult_2?.Value as string;
            string badrequestData_2 = badrequestActionResult_2?.Value as string;

            // Assert
            Assert.NotNull(okData_1);
            Assert.Null(badrequestData_1);
            Assert.Null(okData_2);
            Assert.NotNull(badrequestData_2);
        }
    }
}
