using AutoMapper;
using AutoMapper.Execution;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<List<ShowedOrderDetailDTO>>> GetAllOrderDetailByOrder([FromQuery] string userId)
        {
            try
            {
                Order order = await _orderRepository.GetOrder(userId);
                List<OrderDetail> listOrderDetails = await _orderDetailRepository.GetOrderDetail(order);
                List<ShowedOrderDetailDTO> showedOrderDetailDTOs = new List<ShowedOrderDetailDTO>();
                for (int i = 0; i < listOrderDetails.Count; i++)
                {
                    Product product = await _productRepository.GetProductById(listOrderDetails[i].ProductId);
                    showedOrderDetailDTOs.Add(new ShowedOrderDetailDTO
                    {
                        Id = listOrderDetails[i].Id,
                        number = listOrderDetails[i].Number,
                        comment = listOrderDetails[i].Comment,
                        DatePurchase = listOrderDetails[i].DatePurchase,
                        rating = (int)listOrderDetails[i].Rating,
                        showedProductDTO = _mapper.Map<ShowedProductDTO>(product),
                        isReviewed = listOrderDetails[i].IsReviewed,
                    });
                }
                showedOrderDetailDTOs = showedOrderDetailDTOs.OrderByDescending(x => x.Id).ToList();
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
        [Authorize]

        public async Task<ActionResult<ShowedOrderDetailDTO>> GetOrderDetail([FromQuery] string userId, [FromRoute] int orderDetailId)
        {
            try
            {
                OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetail(orderDetailId);
                if (orderDetail == null)
                {
                    return BadRequest("Invalid OrderDetailID");
                }
                Product product = await _productRepository.GetProductById(orderDetail.ProductId);
                ShowedOrderDetailDTO showedOrderDetailDTO = new ShowedOrderDetailDTO
                {
                    Id = orderDetail.Id,
                    number = orderDetail.Number,
                    comment = orderDetail.Comment,
                    rating = (int)orderDetail.Rating,
                    DatePurchase = orderDetail.DatePurchase,
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
        [Authorize]
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
                        ProductId = item.productId,
                        Product = product,
                        OrderId = order.Id,
                        Order = order,
                        Number = item.number,
                        DatePurchase = DateTime.Today,
                        Comment = "",
                        Rating = (ProductRating)0
                    };
                    CartDetail cartDetail = await _cartDetailRepository.GetCartDetail(cart.Id, item.productId);
                    if (cartDetail != null && cartDetail.Number == item.number)
                    {
                        product.InventoryNumber = product.InventoryNumber - cartDetail.Number;
                        await _productRepository.Save();
                        order.Total += (product.Price * item.number);
                        await _orderDetailRepository.CreateOrderDetail(orderDetail);
                        await _orderDetailRepository.Save();
                        _cartDetailRepository.DeleteCartDetail(cartDetail);
                        await _cartDetailRepository.Save();
                    }
                    else
                    {
                        return BadRequest("Product " + product.ProductName + " with number of product " + item.number + " is invalid");
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
        [Authorize]
        public async Task<ActionResult> UpdateOrderDetail([FromQuery] string userId, [FromRoute] int orderDetailId, [FromBody] ReviewOrderDetailDTO reviewOrderDetailDTO)
        {
            try
            {
                OrderDetail orderDetail = await _orderDetailRepository.GetOrderDetail(orderDetailId);
                if (orderDetail == null)
                {
                    return BadRequest("Invalid OrderDetailID");
                }
                orderDetail.Rating = (ProductRating)reviewOrderDetailDTO.rating;
                orderDetail.Comment = reviewOrderDetailDTO.comment;
                orderDetail.IsReviewed = true;
                _orderDetailRepository.UpdateOrderDetail(orderDetail);
                // Update Product Rating
                await _productRepository.UpdateProductRating(orderDetail.ProductId);
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
