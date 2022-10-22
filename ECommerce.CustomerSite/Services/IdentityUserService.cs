using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO.Account;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IIdentityUser identityUser;

        // Initialize
        public IdentityUserService()
        {
            identityUser = RestService.For<IIdentityUser>("https://localhost:7173");
        }


        // Methods
        public async Task<String> Register(RegisterRequestDTO registerRequestDTO)
        {
           return await identityUser.Register(registerRequestDTO);
        }


        public async Task<String> Login(LoginRequestDTO loginRequestDTO)
        {
            return await identityUser.Login(loginRequestDTO);
        }
    }
}
