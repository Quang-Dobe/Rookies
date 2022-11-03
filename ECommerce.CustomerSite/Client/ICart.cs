using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ECommerce.CustomerSite.Client
{
    [Headers("Content-Type: application/json")]
    public interface ICart
    {
        [Get("/CartDetail/GetAllCart")]
        Task<List<ShowedCartDetailDTO>> GetAllCart();


        [Get("/CartDetail/GetAllCardDetailByCart")]
        Task<List<ShowedCartDetailDTO>> GetAllCardDetailByCart([Query] string userId, [Header("Authorization")] string jwt);


        [Get("/CartDetail/CreateCartDetail/{productId}")]
        Task<string> CreateCartDetail([Query] string userId, int productId, [Body] int number);


        [Get("/CartDetail/UpdateCartDetail/{productId}")]
        Task<string> UpdateCartDetail([Query] string userId, int productId, [Body] int number, [Header("Authorization")] string jwt);


        [Get("/CartDetail/DeleteCartDetail/{productId}")]
        Task<string> DeleteCartDetail([Query] string userId, int productId, [Body] int number, [Header("Authorization")] string jwt);
    }
}
