using AutoMapper;
using ECommerce.Data.Data;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private ECommerceDBContext _dbContext;
        private bool disposed = false;


        // Initialize
        public CartRepository(ECommerceDBContext eCommerceDBContext, IMapper mapper)
        {
            _dbContext = eCommerceDBContext;
        }


        // Methods

        public async Task<List<Cart>> GetCart()
        {
            return await _dbContext.carts.ToListAsync();
        }

        public async Task<Cart> GetCart(int id)
        {
            return await _dbContext.carts.FindAsync(id);
        }

        public async Task<Cart> GetCart(string userId)
        {
            return await _dbContext.carts.Where(Cart => Cart.userId == userId).SingleOrDefaultAsync();
        }

        public void AddCartDetail(Cart cart, CartDetail cartDetail)
        {
            cart.cartDetails.Add(cartDetail);
            UpdateCart(cart);
        }

        public void DelCartDetail(Cart cart, CartDetail cartDetail)
        {
            cart.cartDetails.Remove(cartDetail);
            UpdateCart(cart);
        }

        public async Task CreateCart(string userId)
        {
            await _dbContext.carts.AddAsync(new Cart
            {
                userId = userId,
                user = await _dbContext.Users.FindAsync(userId),
                cartDetails = new List<CartDetail>(),
                total = 0
            });
        }

        public async Task CreateCart(Cart cart)
        {
            await _dbContext.carts.AddAsync(cart);
        }

        public void UpdateCart(Cart cart)
        {
            _dbContext.Entry(cart).State = EntityState.Modified;
        }

        public void DeleteCart(Cart cart)
        {
            _dbContext.carts.Remove(cart);
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
