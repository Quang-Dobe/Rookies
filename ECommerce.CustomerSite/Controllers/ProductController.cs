using ECommerce.CustomerSite.Services.Interface;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;
            string userName = User.Claims.FirstOrDefault(u => u.Type == "userName")?.Value;

            detailProductDTO productDTO = await productService.GetProductByID(id);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["userName"] = userName;
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(productDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart([FromForm] string productId, [FromForm] string number)
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;

            //Console.WriteLine(userId + " - " + int.Parse(productId) + " - " + int.Parse(number) + " - " + jwt);
            await cartService.CreateCartDetail(userId, int.Parse(productId), int.Parse(number), jwt);

            return RedirectToAction("Buy");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Buy()
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;
            string userName = User.Claims.FirstOrDefault(u => u.Type == "userName")?.Value;


            List<ShowedCartDetailDTO> showedCartDetailDTOs = await cartService.GetAllCardDetailByCart(userId, jwt);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["userName"] = userName;
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(showedCartDetailDTOs);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteBuy([FromForm] string productId, [FromForm] string number)
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;

            await cartService.DeleteCartDetail(userId, int.Parse(productId), int.Parse(number), jwt);
            return RedirectToAction("Buy");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToOrder()
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;

            IFormCollection formData = HttpContext.Request.Form;
            List<OrderDetailDTO> orderDetailDTOs = new List<OrderDetailDTO>();
            foreach(var item in formData.ElementAt(0).Value)
            {
                orderDetailDTOs.Add(new OrderDetailDTO
                {
                    productId = int.Parse(item.Split('=')[0]),
                    number = 1
                });
            }

            int i = 0;
            foreach(var item in formData.ElementAt(1).Value)
            {
                orderDetailDTOs.ElementAt(i).number = int.Parse(item.Split('=')[0]);
                i += 1;
            }
            await orderService.CreateOrderDetail(userId, orderDetailDTOs, jwt);

            return RedirectToAction("History");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> History()
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;
            string userName = User.Claims.FirstOrDefault(u => u.Type == "userName")?.Value;

            List<ShowedOrderDetailDTO> showedOrderDetailDTOs = await orderService.GetAllOrderDetailByOrder(userId, jwt);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["userName"] = userName;
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
        [Authorize]
        public async Task<IActionResult> Review([FromQuery] int id)
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;
            string userName = User.Claims.FirstOrDefault(u => u.Type == "userName")?.Value;

            ShowedOrderDetailDTO showedOrderDetailDTO = await orderService.GetOrderDetail(userId, id, jwt);
            List<AllCategoryDTO> allCategoryDTOs = await categoryService.GetAllCategories();
            ViewData["userName"] = userName;
            ViewData["AllCategory"] = allCategoryDTOs;
            ViewData["userId"] = userId;
            ViewData["jwt"] = jwt;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["isAuthorized"] = "true";
            }
            return View(showedOrderDetailDTO);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview([FromQuery] int id, ReviewOrderDetailDTO model)
        {
            string jwt = User.Claims.FirstOrDefault(u => u.Type == "jwt")?.Value;
            string userId = User.Claims.FirstOrDefault(u => u.Type == "userId")?.Value;

            ReviewOrderDetailDTO reviewOrderDetailDTO = new ReviewOrderDetailDTO()
            {
                comment = (model.comment == null || model.comment == "") ? "Ok" : model.comment,
                rating = (model.rating == null || model.rating == 0) ? 4 : model.rating
            };
            if (ModelState.IsValid)
            {
                await orderService.UpdateOrderDetail(userId, id, model, jwt);
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
