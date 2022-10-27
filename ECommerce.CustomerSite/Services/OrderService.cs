using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrder _order;


        // Initialize
        public OrderService()
        {
            _order = RestService.For<IOrder>("https://localhost:7173");
        }


        // Methods
        public Task<List<ShowedOrderDetailDTO>> GetAllOrderDetailByOrder(string userId)
        {
            return _order.GetAllOrderDetailByOrder(userId);
        }

        public Task<ShowedOrderDetailDTO> GetOrderDetail(string userId, int orderDetailId)
        {
            return _order.GetOrderDetail(userId, orderDetailId);
        }
    }
}
