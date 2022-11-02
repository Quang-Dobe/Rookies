using ECommerce.SharedView.DTO.AdminSiteDTO;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<AllCategoryDTO>> GetAllCategories();
    }
}
