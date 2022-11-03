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
        public CartService()
        {
            _cartInterface = RestService.For<ICart>("https://localhost:7173");
        }


        // Methods
        public Task<string> CreateCartDetail(string userId, int productId, int number)
        {
            return _cartInterface.CreateCartDetail(userId, productId, number);
        }

        public Task<string> DeleteCartDetail(string userId, int productId, int number, string jwt)
        {
            return _cartInterface.DeleteCartDetail(userId, productId, number, jwt);
        }

        public Task<List<ShowedCartDetailDTO>> GetAllCardDetailByCart(string userId, string jwt)
        {
            return _cartInterface.GetAllCardDetailByCart(userId, jwt);
        }

        public Task<List<ShowedCartDetailDTO>> GetAllCart()
        {
            return _cartInterface.GetAllCart();
        }

        public Task<string> UpdateCartDetail(string userId, int productId, int number, string jwt)
        {
            return _cartInterface.UpdateCartDetail(userId, productId, number, jwt);
        }
    }
}
