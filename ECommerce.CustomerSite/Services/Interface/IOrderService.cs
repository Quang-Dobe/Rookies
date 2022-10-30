﻿using ECommerce.SharedView.DTO;
using Refit;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface IOrderService
    {
        Task<List<ShowedOrderDetailDTO>> GetAllOrderDetailByOrder(string userId);

        Task<ShowedOrderDetailDTO> GetOrderDetail(string userId, int orderDetailId);
    }
}