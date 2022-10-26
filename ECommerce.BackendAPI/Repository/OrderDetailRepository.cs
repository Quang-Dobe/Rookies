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
        public async Task<List<OrderDetail>> GetOrderDetail()
        {
            return await _dbContext.orderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            return await _dbContext.orderDetails.FindAsync(id);
        }

        public async Task<OrderDetail> GetOrderDetail(int orderId, int productId)
        {
            OrderDetail orderDetail = await (from oD in _dbContext.orderDetails
                                           where oD.orderId == orderId && oD.productId == productId
                                           select oD).FirstOrDefaultAsync();
            return orderDetail;
        }

        public async Task<List<OrderDetail>> GetOrderDetail(Product productId)
        {
            return await _dbContext.orderDetails.Where(orderDetail => orderDetail.productId == productId.Id).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetail(Order orderId)
        {
            return await _dbContext.orderDetails.Where(orderDetail => orderDetail.orderId == orderId.Id).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetListOrderDetailByProduct(List<int> listProductId)
        {
            List<OrderDetail> data = await (from oD in _dbContext.orderDetails
                                            where listProductId.Any(id => id == oD.productId)
                                            select oD).ToListAsync();
            return data;
        }

        public async Task CreateOrderDetail(OrderDetail orderDetail)
        {
            await _dbContext.orderDetails.AddAsync(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _dbContext.Entry(orderDetail).State = EntityState.Modified;
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
