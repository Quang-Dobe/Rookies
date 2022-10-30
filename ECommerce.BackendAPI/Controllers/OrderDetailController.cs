using AutoMapper;
using AutoMapper.Execution;
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
        private readonly IMapper _mapper;


        // Initialize
        public OrderDetailController(IOrderRepository orderRepository, 
            IOrderDetailRepository orderDetailRepository, 
            ICartDetailRepository cartDetailRepository, 
            ICartRepository cartRepository, 
            IProductRepository productRepository,
            IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._cartDetailRepository = cartDetailRepository;
            this._cartRepository = cartRepository;
            this._productRepository = productRepository;
            this._mapper = mapper;
        }


        // Methods
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<List<ShowedOrderDetailDTO>>> GetAllOrderDetailByOrder([FromQuery] string userId)
        {
            try
            {
                Order order = await _orderRepository.GetOrder(userId);
                List<OrderDetail> listOrderDetails = await _orderDetailRepository.GetOrderDetail(order);
                List<ShowedOrderDetailDTO> showedOrderDetailDTOs = new List<ShowedOrderDetailDTO>();
                for (int i = 0; i < listOrderDetails.Count; i++)
                {
                    Product product = await _productRepository.GetProductById(listOrderDetails[i].productId);
                    showedOrderDetailDTOs.Add(new ShowedOrderDetailDTO
                    {
                        Id = listOrderDetails[i].Id,
                        number = listOrderDetails[i].number,
                        comment = listOrderDetails[i].comment,
                        rating = (int)listOrderDetails[i].rating,
                        showedProductDTO = _mapper.Map<ShowedProductDTO>(product)
                    });
                }
                return Ok(showedOrderDetailDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [Route("{orderDetailId:int}")]
        public async Task<ActionResult<ShowedOrderDetailDTO>> GetOrderDetail([FromQuery] string userId, [FromRoute] int orderDetailId)
        {
            try
            {
                OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetail(orderDetailId);
                Product product = await _productRepository.GetProductById(orderDetail.productId);
                ShowedOrderDetailDTO showedOrderDetailDTO = new ShowedOrderDetailDTO
                {
                    Id = orderDetail.Id,
                    number = orderDetail.number,
                    comment = orderDetail.comment,
                    rating = (int)orderDetail.rating,
                    showedProductDTO = _mapper.Map<ShowedProductDTO>(product)
                };
                return Ok(showedOrderDetailDTO);
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
                        return BadRequest("Product " + product.productName + " with number of product " + item.number + " is invalid");
                    }
                }
                await _orderRepository.Save();
                return Ok("Order sucessfully!");
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

                return Ok("Update OrderDetail sucessfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
