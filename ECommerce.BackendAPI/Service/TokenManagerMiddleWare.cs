using System.Net;

namespace ECommerce.BackendAPI.Service
{
    public class TokenManagerMiddleWare : IMiddleware
    {
        private readonly ITokenManager _tokenManager;

        public TokenManagerMiddleWare(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenManager.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
