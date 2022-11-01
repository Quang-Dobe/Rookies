using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Data;
using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using ECommerce.SharedView.Enum;
using Microsoft.AspNetCore.Cors;
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
        [EnableCors("_myAdminSite")]
        [Route("{type:int}")]
        public async Task<ActionResult<List<ShowedProductDTO>>> GetProductByType([FromRoute] int type)
        {
            List<Product> listProducts = await _productRepository.GetProductByType(type);
            List<ShowedProductDTO> showedProductDTOs = _mapper.Map<List<ShowedProductDTO>>(listProducts);
            return Ok(showedProductDTOs);
        }
        

        [HttpGet]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult<List<AllProductDTO>>> GetAllProducts()
        {
            List<Product> allProduct = await _productRepository.GetProducts();
            List<AllProductDTO> allProductDTO = _mapper.Map<List<AllProductDTO>>(allProduct);
            return Ok(allProductDTO);
        }


        [HttpGet]
        [Route("{id:int}")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult<detailProductDTO>> GetProductByID([FromRoute] int id)
        {
            detailProductDTO data = await _productRepository.GetProductDetailById(id);
            if (data is null)
            {
                return BadRequest("No product with given ID!");
            }
            return Ok(data);
        }
        
        
        [HttpPost]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult> CreateNewProduct(AllProductDTO allProductDTO)
        {
            Product product = _mapper.Map<Product>(allProductDTO);
            await _productRepository.CreateProduct(product);
            await _productRepository.Save();
            return Ok("Success: Create successfully!");
        }


        [HttpPost]
        [Route("{id:int}")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] AllProductDTO allProductDTO)
        {
            try
            {
                Product product = await _productRepository.GetProductById(id);
                if (product != null)
                {
                    product.Id = allProductDTO.id;
                    product.ProductImg = allProductDTO.ProductImg;
                    product.ProductName = allProductDTO.ProductName;
                    product.Description = allProductDTO.Description;
                    product.ProductType = (ECommerce.Data.Enums.ProductType)allProductDTO.ProductType;
                    product.Price = allProductDTO.Price;
                    product.Quantity = allProductDTO.Quantity;
                    product.InventoryNumber = allProductDTO.InventoryNumber;
                    product.Rating = allProductDTO.Rating;
                    product.createdDate = allProductDTO.createdDate;
                    product.updatedDate = allProductDTO.updatedDate;
                    _productRepository.UpdateProduct(product);
                    await _productRepository.Save();
                    return Ok("Update sucessfully!");
                }
                return BadRequest("Invalid product");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                Product product = await _productRepository.GetProductById(id);
                if (product != null)
                {
                    await _productRepository.DeleteProduct(id);
                    await _productRepository.Save();
                    return Ok("Delete sucessfully!");
                }
                return BadRequest("Invalid product!");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


}
