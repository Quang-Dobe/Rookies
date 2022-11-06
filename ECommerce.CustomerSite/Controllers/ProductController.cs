﻿using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ECommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;
        private readonly IOrderService orderService;
        private readonly ICategoryService categoryService;

        public ProductController(
            ILogger<HomeController> logger, 
            IProductService productService, 
            ICartService cartService, 
            IOrderService orderService, 
            ICategoryService categoryService)
        {
            this._logger = logger;
            this.productService = productService;
            this.cartService = cartService;
            this.orderService = orderService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int id)
        {
            string userId = Request.Cookies["userId"];
            string jwt = Request.Cookies["jwt"];
            detailProductDTO productDTO = await productService.GetProductByID(id);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Buy()
        {
            string userId = Request.Cookies["userId"];
            string jwt = Request.Cookies["jwt"];
            List<ShowedCartDetailDTO> showedCartDetailDTOs = await cartService.GetAllCardDetailByCart(userId);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(showedCartDetailDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            string userId = Request.Cookies["userId"];
            string jwt = Request.Cookies["jwt"];
            List<ShowedOrderDetailDTO> showedOrderDetailDTOs = await orderService.GetAllOrderDetailByOrder(userId);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(showedOrderDetailDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Review([FromQuery] int id)
        {
            string userId = Request.Cookies["userId"];
            string jwt = Request.Cookies["jwt"];
            ShowedOrderDetailDTO showedOrderDetailDTO = await orderService.GetOrderDetail(userId, id);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(showedOrderDetailDTO);
        }
    }
}
