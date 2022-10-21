using ECommerce.Data.Data;
using ECommerce.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private ECommerceDBContext _dbContext;
        private bool disposed = false;


        // Initialize
        public OrderRepository(ECommerceDBContext eCommerceDBContext)
        {
            _dbContext = eCommerceDBContext;
        }


        // Methods

        public async Task<IEnumerable<Order>> GetOrder()
        {
            return await _dbContext.orders.ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _dbContext.orders.FindAsync(id);
        }

        public async Task<Order> GetOrder(IdentityUser user)
        {
            return await _dbContext.orders.Where(order => order.userID.Id == user.Id).SingleOrDefaultAsync();
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
