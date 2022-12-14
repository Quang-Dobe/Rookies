using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ECommerce.CustomerSite.Client
{
    public interface IOrder
    {
        [Get("/OrderDetail/GetAllOrderDetailByOrder")]
        Task<List<ShowedOrderDetailDTO>> GetAllOrderDetailByOrder([Query] string userId, [Header("Authorization")] string jwt);


        [Get("/OrderDetail/GetOrderDetail/{orderDetailId}")]
        Task<ShowedOrderDetailDTO> GetOrderDetail([Query] string userId, int orderDetailId, [Header("Authorization")] string jwt);

        [Post("/OrderDetail/CreateOrderDetail")]
        Task CreateOrderDetail([Query] string userId, [Body] List<OrderDetailDTO> orderDetailDTOs, [Header("Authorization")] string jwt);

        [Post("/OrderDetail/UpdateOrderDetail/{orderDetailId}")]
        Task UpdateOrderDetail([Query] string userId, int orderDetailId, [Body] ReviewOrderDetailDTO reviewOrderDetailDTO, [Header("Authorization")] string jwt);
    }
}
