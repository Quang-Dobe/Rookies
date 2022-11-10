using ECommerce.CustomerSite.Client;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO.Account;
using Refit;

namespace ECommerce.CustomerSite.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IIdentityUser _identityUser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Initialize
        public IdentityUserService(IHttpContextAccessor httpContextAccessor)
        {
            _identityUser = RestService.For<IIdentityUser>("https://localhost:7173");
            _httpContextAccessor = httpContextAccessor;
        }


        // Methods
        public async Task<String> Register(RegisterRequestDTO registerRequestDTO)
        {
            return await _identityUser.Register(registerRequestDTO);
        }


        public async Task<String> Login(LoginRequestDTO loginRequestDTO)
        {
            return await _identityUser.Login(loginRequestDTO);
        }


        public async Task<String> LogOut(string jwt)
        {
            return await _identityUser.LogOut(jwt);
        }
    }
}
