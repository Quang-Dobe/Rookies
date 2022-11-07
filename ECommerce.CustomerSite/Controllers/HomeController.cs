using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Authorization;
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
        private IProductService _productService;
        private ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            this._productService = productService;
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = Request.Cookies["userId"];
            List<AllCategoryDTO> allCategoryDTOs = await _categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Category([FromQuery] string? type)
        {
            string userId = Request.Cookies["userId"];
            List<ShowedProductDTO> listProducts = await _productService.GetProductByType(int.Parse(type));
            List<AllCategoryDTO> allCategoryDTOs = await _categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
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