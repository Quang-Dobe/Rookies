using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

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
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;
            string userName = User.Claims.FirstOrDefault(u => u.Type == "userName")?.Value;

            List<AllCategoryDTO> allCategoryDTOs = await _categoryService.GetAllCategories();
            ViewData["userName"] = userName;
            ViewData["AllCategory"] = allCategoryDTOs;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Category([FromQuery] string? type, [FromQuery] string? pageIndex)
        {
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;
            string userName = User.Claims.FirstOrDefault(u => u.Type == "userName")?.Value;

            // Call API
            List<AllCategoryDTO> allCategoryDTOs = await _categoryService.GetAllCategories();
            ShowedListProductDTO showedListProductDTO = await _productService.GetProductByTypeWithPageIndex(int.Parse(type), int.Parse(pageIndex));

            ViewData["userName"] = userName;
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["TotalPage"] = showedListProductDTO.totalProductDTO.ToString();
            ViewData["PageIndex"] = pageIndex;
            ViewData["CategoryType"] = type;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(showedListProductDTO.showedProductDTOs);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}