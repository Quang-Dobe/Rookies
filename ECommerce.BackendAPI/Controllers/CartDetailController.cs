using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CartDetailController : Controller
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;


        // Initialize
        public CartDetailController(ICartDetailRepository cartDetailRepository, IProductRepository productRepository, ICartRepository cartRepository, IMapper mapper)
        {
            _cartDetailRepository = cartDetailRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }


        // Methods

        // CartDetail
        [HttpGet]
        public async Task<ActionResult<ShowedCartDetailDTO>> GetCartDetail([FromQuery] int id)
        {
            try
            {
                CartDetail cartDetail = await _cartDetailRepository.GetCartDetail(id);
                ShowedCartDetailDTO data = _mapper.Map<ShowedCartDetailDTO>(cartDetail);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<List<ShowedCartDetailDTO>>> GetAllCartDetail()
        {
            try
            {
                List<CartDetail> listCartDetail = await _cartDetailRepository.GetCartDetail();
                List<ShowedCartDetailDTO> data = _mapper.Map<List<ShowedCartDetailDTO>>(listCartDetail);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ShowedCartDetailDTO>>> GetAllCardDetailByCart([FromQuery] string userid)
        {
            try
            {
                Cart cart = await _cartRepository.GetCart(userid);
                List<CartDetail> listCartDetail = await _cartDetailRepository.GetCartDetail(cart);
                List<ShowedCartDetailDTO> showedCartDetailDTOs = new List<ShowedCartDetailDTO>();
                for (int i = 0; i < listCartDetail.Count; i++)
                {
                    Product product = await _productRepository.GetProductById(listCartDetail[i].ProductId);
                    showedCartDetailDTOs.Add(new ShowedCartDetailDTO
                    {
                        Id = listCartDetail[i].Id,
                        number = listCartDetail[i].Number,
                        showedProductDTO = _mapper.Map<ShowedProductDTO>(product)
                    });
                }
                return Ok(showedCartDetailDTOs);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        [Route("{productId:int}")]
        public async Task<ActionResult> CreateCartDetail([FromQuery] string userId, [FromRoute] int productId, [FromBody] int number)
        {
            try
            {
                // I dont check cart exist or not because when a user register, I also create an empty cart for him/her
                Cart cart = await _cartRepository.GetCart(userId);
                Product product = await _productRepository.GetProductById(productId);
                CartDetail checkCartDetail = await _cartDetailRepository.GetCartDetail(cart.Id, productId);
                if (checkCartDetail != null)
                {
                    checkCartDetail.Number += number;
                    await _cartDetailRepository.Save();
                    return Ok("Update your cart instead of adding new one");
                }
                else
                {
                    CartDetail cartDetail = new CartDetail
                    {
                        ProductId = productId,
                        Product = product,
                        CartId = cart.Id,
                        // If I do this, it will automatically add this CartDetail into List<CartDetail> of user's Cart
                        Cart = cart,
                        Number = number
                    };
                    await _cartDetailRepository.CreateCartDetail(cartDetail);
                    // Because the Save()-methods in all Repository is same, so we just need only one Save()
                    await _cartDetailRepository.Save();

                    return Ok("Create CartDetail sucessfully");
                }
            }
            catch(Exception ex)
            {
                // I will modified this response here in near future (It's look like litle poor :D)
                return StatusCode(500, ex.Message);
            }
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        [Route("{productId:int}")]
        public async Task<ActionResult> UpdateCartDetail([FromQuery] string userId, [FromRoute] int productId, [FromBody] int number)
        {
            try
            {
                // Same as funtion CreateCartDetail above, I dont need to check out
                Cart cart = await _cartRepository.GetCart(userId);
                CartDetail cartDetail = await _cartDetailRepository.GetCartDetail(cart.Id, productId);
                // But here I have to do it
                if (cartDetail == null)
                {
                    return BadRequest("Your cart is empty to update something inside!");
                }
                cartDetail.Number = number;
                _cartDetailRepository.UpdateCartDetail(cartDetail);
                await _cartDetailRepository.Save();

                return Ok("Update CartDetail sucessfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpDelete]
        [Route("{productId:int}")]
        public async Task<ActionResult> DeleteCartDetail([FromQuery] string userId, [FromRoute] int productId, [FromBody] int number)
        {
            try
            {
                Cart cart = await _cartRepository.GetCart(userId);
                CartDetail cartDetail = await _cartDetailRepository.GetCartDetail(cart.Id, productId);
                if (cartDetail == null)
                {
                    return BadRequest("Your cart is empty to do something inside");
                }
                _cartDetailRepository.DeleteCartDetail(cartDetail);
                await _cartDetailRepository.Save();

                return Ok("Delete CartDetail sucessfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
