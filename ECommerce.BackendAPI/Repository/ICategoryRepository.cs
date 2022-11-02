using ECommerce.Data.Model;

namespace ECommerce.BackendAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);

        Task CreateCategory(string name, string description);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);

        Task Save();

    }
}
