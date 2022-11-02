using ECommerce.SharedView.DTO.AdminSiteDTO;
using Refit;

namespace ECommerce.CustomerSite.Client
{
    [Headers("Content-Type: application/json")]
    public interface ICategory
    {
        [Get("/api/Category")]
        Task<List<AllCategoryDTO>> GetAllCategory();
    }
}
