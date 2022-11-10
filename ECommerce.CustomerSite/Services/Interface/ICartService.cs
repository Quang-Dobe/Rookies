using ECommerce.SharedView.DTO;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface ICartService
    {
        Task<List<ShowedCartDetailDTO>> GetAllCart();
        Task<List<ShowedCartDetailDTO>> GetAllCardDetailByCart(string userId, string jwt);
        Task CreateCartDetail(string userId, int productId, int number, string jwt);
        Task UpdateCartDetail(string userId, int productId, int number, string jwt);
        Task DeleteCartDetail(string userId, int productId, int number, string jwt);
    }
}
