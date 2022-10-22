using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService productService;

        public ProductController(ILogger<HomeController> logger, IProductService productService)
        {
            this._logger = logger;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int id)
        {
            detailProductDTO productDTO = await productService.GetProductByID(id);
            return View(productDTO);
        }
    }
}
