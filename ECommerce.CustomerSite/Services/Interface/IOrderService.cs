using ECommerce.SharedView.DTO;
using Refit;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface IOrderService
    {
        Task<List<ShowedOrderDetailDTO>> GetAllOrderDetailByOrder(string userId, string jwt);

        Task<ShowedOrderDetailDTO> GetOrderDetail(string userId, int orderDetailId, string jwt);

        Task CreateOrderDetail(string userId, List<OrderDetailDTO> orderDetailDTOs, string jwt);

        Task UpdateOrderDetail(string userId, int orderDetailId, ReviewOrderDetailDTO reviewOrderDetailDTO, string jwt);
    }
}
