﻿using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface ICartService
    {
        Task<List<ShowedCartDetailDTO>> GetAllCart();
        Task<List<ShowedCartDetailDTO>> GetAllCardDetailByCart(string userId, string jwt);
        Task<string> CreateCartDetail(string userId, int productId, int number);
        Task<string> UpdateCartDetail(string userId, int productId, int number, string jwt);
        Task<string> DeleteCartDetail(string userId, int productId, int number, string jwt);
    }
}
