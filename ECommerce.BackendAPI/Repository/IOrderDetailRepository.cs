using ECommerce.Data.Model;

namespace ECommerce.BackendAPI.Repository
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetOrderDetails();

        Task<OrderDetail> GetOrderDetail(int id);
        Task<List<OrderDetail>> GetOrderDetail(Product productId);
        Task<List<OrderDetail>> GetOrderDetail(Order orderId);
        Task<List<OrderDetail>> GetListOrderDetailByProduct(List<int> listProductId);
        Task<List<OrderDetail>> GetListOrderDetailByOrder(List<int> listOrderId);
        Task Save();
    }
}
