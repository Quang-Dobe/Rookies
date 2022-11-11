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
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IMapper _mapper;


        // Initialize
        public CartController(ICartRepository cartRepository, ICartDetailRepository cartDetailRepository, IMapper mapper)
        {
            this._cartRepository = cartRepository;
            this._cartDetailRepository = cartDetailRepository;
            this._mapper = mapper;
        }


        // Methods
        // Cart

        // Maybe this method will never be called :D
        [DisableCors]
        [HttpPost]
        public async Task<ActionResult> CreateCart([FromQuery] string userId)
        {
            try
            {
                await _cartRepository.CreateCart(userId);
                await _cartRepository.Save();
                return Ok("Create sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [DisableCors]
        // Maybe this method is useless :D
        [HttpGet]
        public async Task<ActionResult<CartDTO>> GetCartByUserId([FromQuery] string userId)
        {
            try
            {
                Cart cart = await _cartRepository.GetCart(userId);
                if (cart == null)
                {
                    return BadRequest("Invalid userId");
                }
                CartDTO cartDTO = _mapper.Map<CartDTO>(cart);
                return Ok(cartDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [DisableCors]
        // Maybe this method is the same as both, useless (Because we just create users when they register but never delete user from database)
        [HttpDelete]
        public async Task<ActionResult> DeleteCart([FromQuery] string userId)
        {
            try
            {
                Cart cart = await _cartRepository.GetCart(userId);
                if (cart == null)
                {
                    return BadRequest("Invalid userId");
                }
                _cartRepository.DeleteCart(cart);
                await _cartRepository.Save();
                return Ok("Delete sucessfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
