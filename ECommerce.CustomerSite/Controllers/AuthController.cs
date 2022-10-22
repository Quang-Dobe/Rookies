using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView;
using ECommerce.SharedView.DTO.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedView.DTO.Account;
using System.Diagnostics;
using System.Text;

namespace ECommerce.CustomerSite.Controllers
{
    [Route("/[controller]/[action]")]
    public class AuthController : Controller
    {
        private IHttpClientFactory clientFactory;
        private readonly IIdentityUserService identityUserService;

        // Intialize
        public AuthController(IIdentityUserService identityUserService)
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
                LoginResponseDTO data = JsonConvert.DeserializeObject<LoginResponseDTO>(stringData);
            
                if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Request.HttpContext.Session.SetString("JWT", data.Value);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return RedirectToAction("Error");
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
                RegisterResponseDTO data = JsonConvert.DeserializeObject<RegisterResponseDTO>(stringData);

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
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
