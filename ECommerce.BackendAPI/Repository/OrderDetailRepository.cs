using ECommerce.Data.Data;
using ECommerce.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private ECommerceDBContext _dbContext;
        private bool disposed = false;


        // Initialize

        public OrderDetailRepository(ECommerceDBContext eCommerceDBContext)
        {
            _dbContext = eCommerceDBContext;
        }


        // Methods

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            return await _dbContext.orderDetails.FindAsync(id);
        }

        public async Task<OrderDetail> GetOrderDetail(Product productId)
        {
            return await _dbContext.orderDetails.Where(orderDetail => orderDetail.productId == productId.Id).FirstOrDefaultAsync();
        }

        public async Task<OrderDetail> GetOrderDetail(Order orderId)
        {
            return await _dbContext.orderDetails.Where(orderDetail => orderDetail.orderId == orderId.Id).FirstOrDefaultAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetails()
        {
            return await _dbContext.orderDetails.ToListAsync();
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
