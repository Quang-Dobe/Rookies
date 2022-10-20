using ECommerce.SharedView;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace ECommerce.CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int a = 0;
            return View(a);
        }

        [HttpGet]
        public async Task<IActionResult> Category([FromQuery] string? type)
        {
            List<ShowedProductDTO> listProducts = new List<ShowedProductDTO>();
            using (var client = clientFactory.CreateClient())
            {
                // Waiting for getting response
                var response = await client.GetAsync("Product/GetProductByType/" + (int)(Enum.Parse(typeof(ProductType), type)));
                // Read data from content
                var content = await response.Content.ReadAsStringAsync();
                // Convert to List of ShowedProductDTO
                listProducts = JsonConvert.DeserializeObject<List<ShowedProductDTO>>(content);
            }
            return View(listProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}