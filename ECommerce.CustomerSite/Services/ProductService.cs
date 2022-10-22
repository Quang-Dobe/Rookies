using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IProduct _productInterface;


        // Intialize
        public ProductService()
        {
            this._productInterface = RestService.For<IProduct>("https://localhost:7173");
        }


        // Service with ProductDTO
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            return await this._productInterface.GetAllProducts();
        }

        public async Task CreateNewProduct(ProductDTO productDTO)
        {
            await this._productInterface.CreateNewProduct(productDTO);
        }

        public async Task DeleteProduct(int id)
        {
            await this._productInterface.DeleteProduct(id);
        }

        public async Task UpdateProduct(ProductDTO productDTO, int id)
        {
            await this._productInterface.UpdateProduct(productDTO, id);
        }


        // Service with ShowedProductDTO
        public async Task<List<ShowedProductDTO>> GetProductByType(int type)
        {
            return await this._productInterface.GetProductByType(type);
        }


        // Service with DetailProductDTO
        public async Task<detailProductDTO> GetProductByID(int id)
        {
            return await this._productInterface.GetProductByID(id);
        }
    }
}
