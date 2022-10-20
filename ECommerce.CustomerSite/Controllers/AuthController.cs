using ECommerce.SharedView.DTO.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedView.DTO.Account;
using System.Text;

namespace ECommerce.CustomerSite.Controllers
{
    [Route("/[controller]/[action]")]
    public class AuthController : Controller
    {
        private IHttpClientFactory clientFactory;

        public AuthController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                // Create a client
                var client = clientFactory.CreateClient();

                var jsonInString = JsonConvert.SerializeObject(model);

                var response = await client.PostAsync("Auth/Login", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginResponseDTO>(contents);

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

            return Redirect("/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDTO model)
        {
            var client = clientFactory.CreateClient();

            var jsonInString = JsonConvert.SerializeObject(model);

            var response = await client.PostAsync("Auth/Register", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<RegisterResponseDTO>(contents);

            if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Failed to register");
            }

            return Redirect("Home/Index");
        }
    }
}
