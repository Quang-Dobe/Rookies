using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
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

        public ProductController(ILogger<HomeController> logger, IProductService productService, ICartService cartService, IOrderService orderService)
        {
            this._logger = logger;
            this.productService = productService;
            this.cartService = cartService;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery] int id)
        {
            detailProductDTO productDTO = await productService.GetProductByID(id);
            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Buy()
        {
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            List<ShowedCartDetailDTO> showedCartDetailDTOs = await cartService.GetAllCardDetailByCart(userId);
            return View(showedCartDetailDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            List<ShowedOrderDetailDTO> showedOrderDetailDTOs = await orderService.GetAllOrderDetailByOrder(userId);
            return View(showedOrderDetailDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Review([FromQuery] int id)
        {
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            ShowedOrderDetailDTO showedOrderDetailDTO = await orderService.GetOrderDetail(userId, id);
            return View(showedOrderDetailDTO);
        }
    }
}
