using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategory _category;


        public CategoryService()
        {
            _category = RestService.For<ICategory>("https://localhost:7173");
        }


        public async Task<List<AllCategoryDTO>> GetAllCategories()
        {
            return await _category.GetAllCategory();
        }
    }
}
