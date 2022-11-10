using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class CartService : ICartService
    {
        private readonly ICart _cartInterface;


        // Initialize
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _cartInterface = RestService.For<ICart>("https://localhost:7173");
        }


        // Methods
        public async Task<List<ShowedCartDetailDTO>> GetAllCardDetailByCart(string userId, string jwt)
        {
            return await _cartInterface.GetAllCardDetailByCart(userId, jwt);
        }

        public async Task<List<ShowedCartDetailDTO>> GetAllCart()
        {
            return await _cartInterface.GetAllCart();
        }

        public async Task CreateCartDetail(string userId, int productId, int number, string jwt)
        {
            await _cartInterface.CreateCartDetail(userId, productId, number, jwt);
        }

        public async Task DeleteCartDetail(string userId, int productId, int number, string jwt)
        {
            await _cartInterface.DeleteCartDetail(userId, productId, number, jwt);
        }

        public async Task UpdateCartDetail(string userId, int productId, int number, string jwt)
        {
            await _cartInterface.UpdateCartDetail(userId, productId, number, jwt);
        }
    }
}
