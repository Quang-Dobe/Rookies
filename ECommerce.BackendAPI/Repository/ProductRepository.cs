using AutoMapper;
using ECommerce.Data.Data;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ECommerceDBContext _dbContext;
        private IMapper _mapper;
        private bool disposed = false;

        public ProductRepository(ECommerceDBContext eCommerceDBContext, IMapper mapper)
        {
            _dbContext = eCommerceDBContext;
            _mapper = mapper;
        }


        // Methods

        public async Task<List<Product>> GetProducts()
        {
            return await _dbContext.products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _dbContext.products.FindAsync(id);
        }

        public async Task<detailProductDTO> GetProductDetailById(int id)
        {
            Product product = await _dbContext.products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            detailProductDTO data = _mapper.Map<detailProductDTO>(product);

            data.reviewProductDTOs = await (from oD in _dbContext.orderDetails
                                            where oD.ProductId == product.Id
                                            join o in _dbContext.orders on oD.OrderId equals o.Id
                                            select new ReviewDTO
                                            {
                                                userName = o.User.UserName,
                                                comment = oD.Comment,
                                                rating = (int)(oD.Rating)
                                            }).ToListAsync();
            return data;
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _dbContext.products.Where(product => product.ProductName == name).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductByType(int type)
        {
            return await _dbContext.products.Where(product => (int)(product.ProductType) == type).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _dbContext.products.AddAsync(product);
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
        }

        public async Task DeleteProduct(int id)
        {
            Product product = await _dbContext.products.FindAsync(id);
            _dbContext.products.Remove(product);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
