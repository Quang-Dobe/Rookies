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


        [Post("/CartDetail/CreateCartDetail/{productId}")]
        Task CreateCartDetail([Query] string userId, int productId, [Body] int number, [Header("Authorization")] string jwt);


        [Post("/CartDetail/UpdateCartDetail/{productId}")]
        Task UpdateCartDetail([Query] string userId, int productId, [Body] int number, [Header("Authorization")] string jwt);


        [Delete("/CartDetail/DeleteCartDetail/{productId}")]
        Task DeleteCartDetail([Query] string userId, int productId, [Body] int number, [Header("Authorization")] string jwt);
    }
}
