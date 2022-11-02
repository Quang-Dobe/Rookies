using ECommerce.Data.Data;
using ECommerce.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ECommerceDBContext _dbContext;
        private bool disposed = false;


        public CategoryRepository(ECommerceDBContext dBContext)
        {
            this._dbContext = dBContext;
        }



        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.categories.ToListAsync();
        }

        public async Task CreateCategory(string name, string description)
        {
            await _dbContext.categories.AddAsync(new Category { Name = name, Description=description }) ;
        }

        public void DeleteCategory(Category category)
        {
            _dbContext.categories.Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _dbContext.categories.FirstAsync(c => c.Id == id);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
