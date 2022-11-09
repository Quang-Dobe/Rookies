using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;

namespace ECommerce.BackendAPI.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<detailProductDTO> GetProductDetailById(int id);
        Task<Product> GetProductByName(string name);
        Task<List<Product>> GetProductByType(int type);
        Task<List<Product>> GetProductByTypeWithPageIndex(int type, int pageIndex);
        Task<int> GetTotalProductByType(int type);
        Task CreateProduct(Product product);
        Task UpdateProductRating(int productId);
        void UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task Save();
    }
}
