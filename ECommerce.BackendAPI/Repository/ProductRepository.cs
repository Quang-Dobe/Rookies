using ECommerce.Data.Data;
using ECommerce.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ECommerceDBContext _dbContext;
        private bool disposed = false;

        public ProductRepository(ECommerceDBContext eCommerceDBContext)
        {
            _dbContext = eCommerceDBContext;
        }


        // Methods

        public async Task<List<Product>> GetProducts()
        {
            return await _dbContext.products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.products.FindAsync(id);
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _dbContext.products.Where(product => product.productName == name).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductByType(int type)
        {
            return await _dbContext.products.Where(product => (int)(product.productType) == type).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _dbContext.products.AddAsync(product);
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
        }

        public async Task DeleteProduct(int id)
        {
            Product product = await _dbContext.products.FindAsync(id);
            _dbContext.products.Remove(product);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
