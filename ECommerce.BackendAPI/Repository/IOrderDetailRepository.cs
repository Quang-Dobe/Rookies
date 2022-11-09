using ECommerce.Data.Model;

namespace ECommerce.BackendAPI.Repository
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetOrderDetail();
        Task<OrderDetail> GetOrderDetail(int id);
        Task<OrderDetail> GetOrderDetail(int orderId, int productId);
        Task<List<OrderDetail>> GetOrderDetail(Product productId);
        Task<List<OrderDetail>> GetOrderDetail(Order orderId);
        Task<int> GetTotalByCategory(int type);
        Task<int> GetTotalOrderByCategory(int type);
        Task<int> GetTotalByDate(DateTime dateTime);
        Task<int> GetTotalOrderByDate(DateTime dateTime);
        Task<List<OrderDetail>> GetListOrderDetailByProduct(List<int> listProductId);
        Task CreateOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        Task Save();
    }
}
