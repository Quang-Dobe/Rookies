using AutoMapper;
using ECommerce.Data.Data;
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
        private readonly ECommerceDBContext eCommerceDBContext;
        private IMapper _mapper;

        public ProductController(ECommerceDBContext eCommerceDBContext, IMapper mapper)
        {
            this.eCommerceDBContext = eCommerceDBContext;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IResult> CreateNewProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await eCommerceDBContext.products.AddAsync(product);
            await eCommerceDBContext.SaveChangesAsync();
            return Results.Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IResult> GetProductByID([FromRoute] int id)
        {
            Product product = await eCommerceDBContext.products.FindAsync(id);
            if (product == null)
            {
                return Results.NotFound("There is no product with given ID");
            }
            //OrderDetail orderDetail = await (from o in eCommerceDBContext.orderDetails
            //                                 where o.productId == product
            //                                 select o).ToListAsync();
            return Results.Ok(product);
        }

        [HttpGet]
        [Route("{type:int}")]
        public async Task<ActionResult<List<ShowedProductDTO>>> GetProductByType([FromRoute] int type)
        {
            List<Product> listProducts = await eCommerceDBContext.products.Where(product => (ProductType)(product.productType) == (ProductType)type).ToListAsync();
            List<ShowedProductDTO> listProductsDTO = _mapper.Map<List<Product>, List<ShowedProductDTO>>(listProducts);
            return Json(listProductsDTO);
        }

        [HttpGet]
        public async Task<IResult> GetProducts()
        {
            IEnumerable<Product> allProducts = await eCommerceDBContext.products.ToListAsync();
            return Results.Ok(allProducts);
        }
    }
}
