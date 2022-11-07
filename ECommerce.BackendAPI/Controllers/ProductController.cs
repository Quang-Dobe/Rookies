using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;


        // Initialization
        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
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
            Category category = await _categoryRepository.GetCategory(type);
            if (category == null)
            {
                return BadRequest("Invalid Category ID");
            }
            List<Product> listProducts = await _productRepository.GetProductByType(type);
            List<ShowedProductDTO> showedProductDTOs = _mapper.Map<List<ShowedProductDTO>>(listProducts);
            return Ok(showedProductDTOs);
        }
        

        [HttpGet]
        [EnableCors("_myAdminSite")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult> CreateNewProduct(AllProductDTO allProductDTO)
        {
            Category category = await _categoryRepository.GetCategory(allProductDTO.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid Category ID");
            }
            await _productRepository.CreateProduct(new Product
            {
                ProductImg = allProductDTO.ProductImg,
                ProductName = allProductDTO.ProductName,
                Description = allProductDTO.Description,
                CategoryId = category.Id,
                Price = allProductDTO.Price,
                Quantity = allProductDTO.Quantity,
                InventoryNumber = allProductDTO.InventoryNumber

            });
            await _productRepository.Save();
            return Ok("Success: Create successfully!");
        }


        [HttpPost]
        [Route("{id:int}")]
        [EnableCors("_myAdminSite")]
        [Authorize]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] AllProductDTO allProductDTO)
        {
            try
            {
                Product product = await _productRepository.GetProductById(id);
                if (product != null)
                {
                    product.ProductImg = allProductDTO.ProductImg;
                    product.ProductName = allProductDTO.ProductName;
                    product.Description = allProductDTO.Description;
                    product.CategoryId = allProductDTO.CategoryId;
                    product.Price = allProductDTO.Price;
                    product.Quantity = allProductDTO.Quantity;
                    product.InventoryNumber = allProductDTO.InventoryNumber;
                    product.Rating = allProductDTO.Rating;
                    product.updatedDate = DateTime.Today;
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


        [HttpPost]
        [EnableCors("_myAdminSite")]
        [Authorize]
        public async Task<ActionResult> UpdateMultiProduct([FromBody] List<AllProductDTO> allProductDTOs)
        {
            try
            {
                foreach (AllProductDTO allProductDTO in allProductDTOs)
                {
                    Product product = await _productRepository.GetProductById(allProductDTO.id);
                    if (product != null)
                    {
                        product.ProductImg = allProductDTO.ProductImg;
                        product.ProductName = allProductDTO.ProductName;
                        product.Description = allProductDTO.Description;
                        product.CategoryId = allProductDTO.CategoryId;
                        product.Price = allProductDTO.Price;
                        product.Quantity = allProductDTO.Quantity;
                        product.InventoryNumber = allProductDTO.InventoryNumber;
                        product.Rating = allProductDTO.Rating;
                        product.updatedDate = DateTime.Today;
                        _productRepository.UpdateProduct(product);
                        await _productRepository.Save();
                    }
                    else
                    {
                        return BadRequest("Invalid product");
                    }
                }
                return Ok("Update sucessfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        [EnableCors("_myAdminSite")]
        [Authorize]
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

        [HttpDelete]
        [EnableCors("_myAdminSite")]
        [Authorize]
        public async Task<ActionResult> DeleteMultiProduct([FromBody] List<int> ids)
        {
            try
            {
                foreach(int id in ids)
                {
                    Product product = await _productRepository.GetProductById(id);
                    if (product != null)
                    {
                        await _productRepository.DeleteProduct(id);
                        await _productRepository.Save();
                    }
                    else
                    {
                        return BadRequest("Invalid product!");
                    }
                }
                return Ok("Delete sucessfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


}
