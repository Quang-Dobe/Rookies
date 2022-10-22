using ECommerce.SharedView.DTO.Account;

namespace ECommerce.CustomerSite.Services.Interface
{
    public interface IIdentityUserService
    {
        Task<String> Register(RegisterRequestDTO registerRequestDTO);
        Task<String> Login(LoginRequestDTO loginRequestDTO);
    }
}
