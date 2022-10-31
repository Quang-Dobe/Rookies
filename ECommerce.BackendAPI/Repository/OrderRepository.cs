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

        public async Task<List<Order>> GetOrder()
        {
            return await _dbContext.orders.ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _dbContext.orders.FindAsync(id);
        }

        public async Task<Order> GetOrder(string userId)
        {
            return await _dbContext.orders.Where(order => order.UserId == userId).SingleOrDefaultAsync();
        }

        public async Task CreateOrder(string userId)
        {
            await _dbContext.orders.AddAsync(new Order
            {
                UserId = userId,
                User = await _dbContext.Users.FindAsync(userId),
                OrderDetails = new List<OrderDetail>(),
                DateOfPurchase = DateTime.Today,
                Total = 0
            });
        }

        public void UpdateOrder(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
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
