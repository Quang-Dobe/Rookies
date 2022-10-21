using ECommerce.Data.Model;

namespace ECommerce.BackendAPI.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByName(string name);
        Task<List<Product>> GetProductByType(int type);
        Task CreateProduct(Product product);
        void UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task Save();
    }
}
