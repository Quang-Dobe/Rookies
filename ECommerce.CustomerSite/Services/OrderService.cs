using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrder _order;
        private readonly IHttpContextAccessor _httpContextAccessor;


        // Initialize
        public OrderService(IHttpContextAccessor httpContextAccessor)
        {
            _order = RestService.For<IOrder>("https://localhost:7173");
            _httpContextAccessor = httpContextAccessor;
        }


        // Methods
        public Task<List<ShowedOrderDetailDTO>> GetAllOrderDetailByOrder(string userId)
        {
            string jwt = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            return _order.GetAllOrderDetailByOrder(userId, jwt);
        }

        public Task<ShowedOrderDetailDTO> GetOrderDetail(string userId, int orderDetailId)
        {
            string jwt = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            return _order.GetOrderDetail(userId, orderDetailId, jwt);
        }
    }
}
