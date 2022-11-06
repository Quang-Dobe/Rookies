using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView;
using ECommerce.SharedView.DTO.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedView.DTO.Account;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerce.CustomerSite.Controllers
{
    [Route("/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IIdentityUserService identityUserService;

        // Intialize
        public AuthController(IHttpClientFactory clientFactory, IIdentityUserService identityUserService)
        {
            this.identityUserService = identityUserService;
        }


        // Methods
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (ModelState.IsValid)
            {
                String stringData = await identityUserService.Login(loginRequestDTO);

                if (stringData != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stringData);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var cookieOption = new CookieOptions()
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        IsEssential = true
                    };
                    Response.Cookies.Append("jwt", "Bearer " + stringData, cookieOption);
                    Response.Cookies.Append("userId", tokenS.Claims.First(claim => claim.Type == "nameid").Value, cookieOption);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return RedirectToAction("Error");
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            String deactivateTokenResult = await identityUserService.LogOut();
            if (deactivateTokenResult == "Deactivate Token")
            {
                Response.Cookies.Delete("jwt");
                Response.Cookies.Delete("userId");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            if (ModelState.IsValid)
            {
                String stringData = await identityUserService.Register(registerRequestDTO);

                if (stringData == "Create sucessfully!")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to register");
                }
            }
            return RedirectToAction("Error");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
