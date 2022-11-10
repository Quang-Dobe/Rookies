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
        public async Task<List<ShowedOrderDetailDTO>> GetAllOrderDetailByOrder(string userId, string jwt)
        {
            return await _order.GetAllOrderDetailByOrder(userId, jwt);
        }

        public async Task<ShowedOrderDetailDTO> GetOrderDetail(string userId, int orderDetailId, string jwt)
        {
            return await _order.GetOrderDetail(userId, orderDetailId, jwt);
        }

        public async Task CreateOrderDetail(string userId, List<OrderDetailDTO> orderDetailDTOs, string jwt)
        {
            await _order.CreateOrderDetail(userId, orderDetailDTOs, jwt);
        }

        public async Task UpdateOrderDetail(string userId, int orderDetailId, ReviewOrderDetailDTO reviewOrderDetailDTO, string jwt)
        {
            await _order.UpdateOrderDetail(userId, orderDetailId, reviewOrderDetailDTO, jwt);
        }
    }
}
