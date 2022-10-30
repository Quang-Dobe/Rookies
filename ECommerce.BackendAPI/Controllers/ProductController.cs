using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Data;
using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;


        // Initialization
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }


        // Methods

        #region This logic is used for writing unit test
        //[HttpGet]
        //public async Task<JsonResult> TestLogicForWritingTest()
        //{
        //    List<Product> allProduct = await _productRepository.GetProducts();
        //    List<ProductDTO> allProductDTO = _mapper.Map<List<ProductDTO>>(allProduct);
        //    return Json(allProductDTO);
        //}
        #endregion


        [HttpGet]
        [Route("{type:int}")]
        public async Task<ActionResult<List<ShowedProductDTO>>> GetProductByType([FromRoute] int type)
        {
            List<Product> listProducts = await _productRepository.GetProductByType(type);
            List<ShowedProductDTO> showedProductDTOs = _mapper.Map<List<ShowedProductDTO>>(listProducts);
            return Ok(showedProductDTOs);
        }
        

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            List<Product> allProduct = await _productRepository.GetProducts();
            List<ProductDTO> allProductDTO = _mapper.Map<List<ProductDTO>>(allProduct);
            return Ok(allProductDTO);
        }


        [HttpPost]
        public async Task<ActionResult> CreateNewProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateProduct(product);
            await _productRepository.Save();
            return Ok("Success: Create successfully!");
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<detailProductDTO>> GetProductByID([FromRoute] int id)
        {
            detailProductDTO data = await _productRepository.GetProductDetailById(id);
            if (data is null)
            {
                return BadRequest("No product with given ID!");
            }
            return Ok(data);
        }
    }


}
