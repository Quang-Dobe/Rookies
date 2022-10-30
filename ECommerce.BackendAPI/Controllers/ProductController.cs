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

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }



        // Methods

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





        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Test()
        {
            return Json(new List<ProductDTO>
            {
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                    productName = "CPU Intel Core I5-7600",
                    description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                    productType = 0,
                    price = 4690
                },
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/UwKfc2vSQGNYIHP23DfTWcToEmsIaxjQsdx0DtIEbqCeZ5dnGBPS7d7WCVW9TOiIkfAh2ddgwDvnOR5U_jg=w500-rw",
                    productName = "CPU INTEL Core i5-10400",
                    description = "Socket: LGA 1200 , Intel Core thế hệ thứ 10",
                    productType = 0,
                    price = 4429
                },
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/3K84fNb4XFMvh7JyJ1-itImN6petr8lxpeLhNCIEpidnZGc0fOIjN5SQiHvWM3InvCFzJjwrpOpK3sY0P95o7ZA4VV-aB1JxiA=w500-rw",
                    productName = "CPU INTEL Core i5-11600K",
                    description = "Socket: 1200, Intel Core thế hệ thứ 11",
                    productType = 0,
                    price = 6099
                }
            });
        }

        [HttpGet]
        public async Task<ActionResult> TestWithString()
        {
            return Json("Thís logic is OK");
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
