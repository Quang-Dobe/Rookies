using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;

namespace ECommerce.BackendAPI.Service
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _distributedCache;


        // Initialization
        public TokenManager(IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache)
        {
            this._contextAccessor = httpContextAccessor;
            this._distributedCache = distributedCache;
        }



        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await _distributedCache.GetStringAsync(GetKey(token)) == null;

        public async Task DeactivateAsync(string token)
            => await _distributedCache.SetStringAsync(GetKey(token),
                " ");


        private string GetCurrentAsync()
        {
            var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["authorization"];
            return authorizationHeader == StringValues.Empty ? StringValues.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
        {
            return $"tokens:{token}:deactivated";
        }
    }
}
