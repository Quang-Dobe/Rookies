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
        private ECommerceDBContext eCommerceDBContext;
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductController(IProductRepository productRepository, ECommerceDBContext eCommerceDBContext, IMapper mapper)
        {
            this._productRepository = productRepository;
            this.eCommerceDBContext = eCommerceDBContext;
            this._mapper = mapper;
        }



        // Methods

        [HttpGet]
        [Route("{type:int}")]
        public async Task<ActionResult<List<ShowedProductDTO>>> GetProductByType([FromRoute] int type)
        {
            List<Product> listProducts = await _productRepository.GetProductByType(type);
            List<ShowedProductDTO> showedProductDTOs = _mapper.Map<List<ShowedProductDTO>>(listProducts);
            return Json(showedProductDTOs);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            List<Product> allProduct = await _productRepository.GetProducts();
            List<ProductDTO> allProductDTO = _mapper.Map<List<ProductDTO>>(allProduct);
            return Json(allProductDTO);
        }


        [HttpPost]
        public async Task<ActionResult> CreateNewProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateProduct(product);
            await _productRepository.Save();
            return Json("Success: Create successfully!");
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<detailProductDTO>> GetProductByID([FromRoute] int id)
        {
            Product product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return Json("Error: No product with given ID!");
            }

            detailProductDTO data = _mapper.Map<detailProductDTO>(product);

            data.reviewProductDTOs = await (from oD in eCommerceDBContext.orderDetails
                                            where oD.productId == product.Id
                                            join o in eCommerceDBContext.orders on oD.orderId equals o.Id
                                            select new ReviewProductDTO
                                            {
                                                userName = o.user.UserName,
                                                comment = oD.comment,
                                                rating = (int)(oD.rating)
                                            }).ToListAsync();
            return Json(data);
        }
    }


}
