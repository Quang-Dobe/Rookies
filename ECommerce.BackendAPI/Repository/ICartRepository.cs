using ECommerce.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.BackendAPI.Repository
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCart();
        Task<Cart> GetCart(int id);
        Task<Cart> GetCart(string userId);
        void AddCartDetail(Cart cart, CartDetail cartDetail);
        void DelCartDetail(Cart cart, CartDetail cartDetail);
        Task CreateCart(string userId);
        Task CreateCart(Cart cart);
        void UpdateCart(Cart cart);
        void DeleteCart(Cart cart);
        Task Save();
    }
}
