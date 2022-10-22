using ECommerce.CustomerSite.Services.Interface;
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
        //private IHttpClientFactory clientFactory;
        private IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Category([FromQuery] string? type)
        {
            // Using with HttpClientFactory
            //List<ShowedProductDTO> listProducts = new List<ShowedProductDTO>();
            //using (var client = clientFactory.CreateClient())
            //{
            //    // Waiting for getting response
            //    var response = await client.GetAsync("Product/GetProductByType/" + (int)(Enum.Parse(typeof(ProductType), type)));
            //    // Read data from content
            //    var content = await response.Content.ReadAsStringAsync();
            //    // Convert to List of ShowedProductDTO
            //    listProducts = JsonConvert.DeserializeObject<List<ShowedProductDTO>>(content);
            //}
            //return View(listProducts);

            List<ShowedProductDTO> listProducts = await productService.GetProductByType((int)(Enum.Parse(typeof(ProductType), type)));
            return View(listProducts);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}