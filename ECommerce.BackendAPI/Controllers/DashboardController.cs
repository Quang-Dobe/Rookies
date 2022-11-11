using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly ICategoryRepository _categoryRepository;


        public DashboardController(IOrderDetailRepository orderDetailRepository, ICategoryRepository categoryRepository, IMapper mapper, ICartDetailRepository cartDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _categoryRepository = categoryRepository;
            _cartDetailRepository = cartDetailRepository;
        }


        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<TotalOrderInCategoryDTO>>> OrderByOrderAndCart()
        {
            try
            {
                List<TotalOrderInCategoryDTO> result = new List<TotalOrderInCategoryDTO>();
                // Get all Category
                List<Category> categories = await _categoryRepository.GetCategories();
                foreach(Category category in categories)
                {
                    int totalOrder = await _orderDetailRepository.GetTotalOrderByCategory(category.Id);
                    int totalCart = await _cartDetailRepository.GetTotalCartDetailByCategory(category.Id);
                    result.Add(new TotalOrderInCategoryDTO
                    {
                        categoryId = category.Id,
                        categoryName = category.Name,
                        total = (totalOrder + totalCart) != 0 ? totalOrder * 1.0 / (totalOrder + totalCart) : 0
                    }); ;
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<int>>> TotalInThreeDays()
        {
            try
            {
                List<int> result = new List<int>();
                result.Add(await _orderDetailRepository.GetTotalByDate(DateTime.Today.AddDays(-1)));
                result.Add(await _orderDetailRepository.GetTotalByDate(DateTime.Today.AddDays(-2)));
                result.Add(await _orderDetailRepository.GetTotalByDate(DateTime.Today.AddDays(-3)));
                result.Reverse();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<int>>> OrderInThreeDays()
        {
            try
            {
                List<int> result = new List<int>();
                result.Add(await _orderDetailRepository.GetTotalOrderByDate(DateTime.Today.AddDays(-1)));
                result.Add(await _orderDetailRepository.GetTotalOrderByDate(DateTime.Today.AddDays(-2)));
                result.Add(await _orderDetailRepository.GetTotalOrderByDate(DateTime.Today.AddDays(-3)));
                result.Reverse();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<TotalInCategoryDTO>>> TotalOfEachCategory()
        {
            try
            {
                List<TotalInCategoryDTO> result = new List<TotalInCategoryDTO>();
                double totalSales = 0;
                // Get all Category
                List<Category> categories = await _categoryRepository.GetCategories();
                foreach(Category category in categories)
                {
                    double sale = await _orderDetailRepository.GetTotalByCategory(category.Id);
                    totalSales += sale;
                    result.Add(new TotalInCategoryDTO
                    {
                        categoryId = category.Id,
                        categoryName = category.Name,
                        total = sale
                    });
                }
                foreach(TotalInCategoryDTO subResult in result)
                {
                    subResult.total = subResult.total / totalSales;
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
