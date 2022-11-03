using ECommerce.SharedView.DTO.Account;
using Refit;

namespace ECommerce.CustomerSite.Client
{
    public interface IIdentityUser
    {
        [Post("/Auth/Register")]
        Task<String> Register([Body] RegisterRequestDTO registerRequestDTO);

        [Post("/Auth/Login")]
        Task<String> Login([Body] LoginRequestDTO loginRequestDTO);

        [Post("/Auth/LogOut")]
        Task<String> LogOut([Header("Authorization")] string jwt);
    }
}
