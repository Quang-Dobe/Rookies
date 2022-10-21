using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpClientFactory clientFactory;

        public ProductController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int id)
        {
            detailProductDTO productDTO = new detailProductDTO();
            using(var client = clientFactory.CreateClient())
            {
                // Waiting for response
                var response = await client.GetAsync("Product/GetProductByID/" + id.ToString());
                // Read data from content
                var content = await response.Content.ReadAsStringAsync();
                // Convert Json to ProductDTO type
                productDTO = JsonConvert.DeserializeObject<detailProductDTO>(content);
            }
            return View(productDTO);
        }
    }
}
