using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;


        // Initialize
        public OrderDetailController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ICartDetailRepository cartDetailRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._cartDetailRepository = cartDetailRepository;
            this._cartRepository = cartRepository;
            this._productRepository = productRepository;
        }


        // Methods
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [Route("{orderDetailId:int}")]
        public async Task<ActionResult> GetOrderDetail([FromQuery] string userId, [FromRoute] int orderDetailId)
        {
            try
            {
                OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetail(orderDetailId);
                Console.Write(orderDetail.Id);
                return StatusCode(200, "Ok you got it!");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        public async Task<ActionResult> CreateOrderDetail([FromQuery] string userId, [FromBody] List<OrderDetailDTO> orderDetailDTOs)
        {
            try
            {
                Order order = await _orderRepository.GetOrder(userId);
                Cart cart = await _cartRepository.GetCart(userId);

                foreach (OrderDetailDTO item in orderDetailDTOs)
                {
                    Product product = await _productRepository.GetProductById(item.productId);
                    OrderDetail orderDetail = new OrderDetail
                    {
                        productId = item.productId,
                        product = product,
                        orderId = order.Id,
                        order = order,
                        number = item.number,
                        datePurchase = DateTime.Today,
                        comment = "",
                        rating = (ProductRating)4
                    };
                    CartDetail cartDetail = await _cartDetailRepository.GetCartDetail(cart.Id, item.productId);
                    if (cartDetail != null && cartDetail.number == item.number)
                    {
                        order.total += (product.price * item.number);
                        await _orderDetailRepository.CreateOrderDetail(orderDetail);
                        await _orderDetailRepository.Save();
                        _cartDetailRepository.DeleteCartDetail(cartDetail);
                        await _cartDetailRepository.Save();
                    }
                    else
                    {
                        return StatusCode(400, "Product " + product.productName + " with number of product " + item.number + " is invalid");
                    }
                }
                await _orderRepository.Save();
                return StatusCode(200, "Order sucessfully!");
            }
            catch(Exception ex)
            {
                return Json(500, ex.InnerException);
            }
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        [Route("{orderDetailId:int}")]
        public async Task<ActionResult> UpdateOrderDetail([FromQuery] string userId, [FromRoute] int orderDetailId, [FromBody] ReviewOrderDetailDTO reviewOrderDetailDTO)
        {
            try
            {
                OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetail(orderDetailId);
                if (orderDetail == null)
                {
                    return BadRequest("Invalid OrderDetailID");
                }
                orderDetail.rating = (ProductRating)reviewOrderDetailDTO.rating;
                orderDetail.comment = reviewOrderDetailDTO.comment;
                _orderDetailRepository.UpdateOrderDetail(orderDetail);
                await _orderDetailRepository.Save();

                return StatusCode(200, "Update OrderDetail sucessfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
