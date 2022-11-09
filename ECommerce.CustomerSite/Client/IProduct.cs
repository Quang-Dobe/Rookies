using ECommerce.SharedView.DTO;
using Refit;

namespace ECommerce.CustomerSite.Client
{
    [Headers("Content-Type: application/json")]
    public interface IProduct
    {
        // ProductDTO
        [Get("/Product/GetAllProducts")]
        Task<List<ProductDTO>> GetAllProducts();


        [Post("/Product/CreateNewProduct")]
        Task CreateNewProduct([Body] ProductDTO productDTO);


        [Delete("/Product/DeleteProduct/{id}")]
        Task DeleteProduct(int id);


        [Put("/Product/UpdateProduct/{id}")]
        Task UpdateProduct([Body] ProductDTO productDTO, int id);


        // ShowedProductDTO
        [Get("/Product/GetProductByType/{type}")]
        Task<List<ShowedProductDTO>> GetProductByType(int type);


        // ShowedProductDTO
        [Get("/Product/GetProductByTypeWithPageIndex/{type}")]
        Task<ShowedListProductDTO> GetProductByTypeWithPageIndex(int type, [Query] int pageIndex);


        // DetailProductDTO
        [Get("/Product/GetProductByID/{id}")]
        Task<detailProductDTO> GetProductByID(int id);
    }
}
