using ECommerce.Data.Data;
using ECommerce.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private ECommerceDBContext _dbContext;
        private bool disposed = false;


        // Initialize

        public CartDetailRepository(ECommerceDBContext eCommerceDBContext)
        {
            _dbContext = eCommerceDBContext;
        }


        // Methods
        public async Task<List<CartDetail>> GetCartDetail()
        {
            return await _dbContext.cartDetails.ToListAsync();
        }

        public async Task<CartDetail> GetCartDetail(int id)
        {
            return await _dbContext.cartDetails.FindAsync(id);
        }

        public async Task<CartDetail> GetCartDetail(int cartId, int productId)
        {
            CartDetail cartDetail = await (from cD in _dbContext.cartDetails
                                           where cD.cartId == cartId && cD.productId == productId
                                           select cD).FirstOrDefaultAsync();
            return cartDetail;
        }

        public async Task<List<CartDetail>> GetCartDetail(Product productId)
        {
            return await _dbContext.cartDetails.Where(CartDetail => CartDetail.productId == productId.Id).ToListAsync();
        }

        public async Task<List<CartDetail>> GetCartDetail(Cart cart)
        {
            return await _dbContext.cartDetails.Where(CartDetail => CartDetail.cartId == cart.Id).ToListAsync();
        }

        public async Task<List<CartDetail>> GetListCartDetailByProduct(List<int> listProductId)
        {
            List<CartDetail> data = await (from cD in _dbContext.cartDetails
                                            where listProductId.Any(id => id == cD.productId)
                                            select cD).ToListAsync();
            return data;
        }

        public async Task CreateCartDetail(CartDetail cartDetail)
        {
            await _dbContext.cartDetails.AddAsync(cartDetail);
        }

        public void UpdateCartDetail(CartDetail cartDetail)
        {
            _dbContext.Entry(cartDetail).State = EntityState.Modified;
        }

        public void DeleteCartDetail(CartDetail cartDetail)
        {
            _dbContext.cartDetails.Remove(cartDetail);
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
