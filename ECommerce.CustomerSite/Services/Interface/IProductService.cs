using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProducts();
        Task CreateNewProduct(ProductDTO productDTO);
        Task DeleteProduct(int id);
        Task UpdateProduct(ProductDTO productDTO, int id);
        Task<List<ShowedProductDTO>> GetProductByType(int type);
        Task<detailProductDTO> GetProductByID(int id);
    }
}
