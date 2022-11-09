using ECommerce.Data.Model;

namespace ECommerce.BackendAPI.Repository
{
    public interface ICartDetailRepository
    {
        Task<List<CartDetail>> GetCartDetail();
        Task<CartDetail> GetCartDetail(int id);
        Task<CartDetail> GetCartDetail(int cartId, int productId);
        Task<List<CartDetail>> GetCartDetail(Product product);
        Task<List<CartDetail>> GetCartDetail(Cart cart);
        Task<int> GetTotalCartDetailByCategory(int type);
        Task<List<CartDetail>> GetListCartDetailByProduct(List<int> listProductId);
        Task CreateCartDetail(CartDetail cartDetail);
        void UpdateCartDetail(CartDetail cartDetail);
        void DeleteCartDetail(CartDetail cartDetail);
        Task Save();
    }
}
