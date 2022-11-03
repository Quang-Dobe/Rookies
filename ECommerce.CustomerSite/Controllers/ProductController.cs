using ECommerce.CustomerSite.Services;
using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;
        private readonly IOrderService orderService;
        private readonly ICategoryService categoryService;

        public ProductController(ILogger<HomeController> logger, IProductService productService, ICartService cartService, IOrderService orderService, ICategoryService categoryService)
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
            detailProductDTO productDTO = await productService.GetProductByID(id);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = GlobalVariable.userId;
            ViewData["jwt"] = GlobalVariable.jwt;
            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Buy()
        {
            string userId = GlobalVariable.userId;
            List<ShowedCartDetailDTO> showedCartDetailDTOs = await cartService.GetAllCardDetailByCart(userId, GlobalVariable.jwt);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = GlobalVariable.userId;
            ViewData["jwt"] = GlobalVariable.jwt;
            return View(showedCartDetailDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            string userId = GlobalVariable.userId;
            List<ShowedOrderDetailDTO> showedOrderDetailDTOs = await orderService.GetAllOrderDetailByOrder(userId, GlobalVariable.jwt);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = GlobalVariable.userId;
            ViewData["jwt"] = GlobalVariable.jwt;
            return View(showedOrderDetailDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Review([FromQuery] int id)
        {
            string userId = GlobalVariable.userId;
            ShowedOrderDetailDTO showedOrderDetailDTO = await orderService.GetOrderDetail(userId, id, GlobalVariable.jwt);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = GlobalVariable.userId;
            ViewData["jwt"] = GlobalVariable.jwt;
            return View(showedOrderDetailDTO);
        }
    }
}
