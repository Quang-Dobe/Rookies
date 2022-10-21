using ECommerce.Data.Model;

namespace ECommerce.BackendAPI.Repository
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetOrderDetails();

        Task<OrderDetail> GetOrderDetail(int id);
        Task<OrderDetail> GetOrderDetail(Product productId);
        Task<OrderDetail> GetOrderDetail(Order orderId);
    }
}
