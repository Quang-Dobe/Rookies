using ECommerce.Data.Data;
using ECommerce.SharedView.DTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BackendAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceDBContext eCommerceDBContext;
        private bool disposed = false;


        // Initialize
        public UserRepository(ECommerceDBContext eCommerceDBContext)
        {
            this.eCommerceDBContext = eCommerceDBContext;
        }


        // Methods

        public async Task<List<IdentityUser>> GetUsers()
        {
            return await eCommerceDBContext.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetUserByName(string userName)
        {
            return await eCommerceDBContext.Users.FirstAsync(x => x.UserName == userName);
        }

        public async Task<IdentityUser> GetUserByEmail(string userEmail)
        {
            return await eCommerceDBContext.Users.FirstAsync(x => x.Email == userEmail);
        }

        public async Task CreateUser(IdentityUser identityUser)
        {
            await eCommerceDBContext.Users.AddAsync(identityUser);
        }

        public void UpdateUser(IdentityUser identityUser)
        {
            eCommerceDBContext.Entry(identityUser).State = EntityState.Modified;
        }

        public async Task DeleteUserByName(IdentityUser user)
        {
            eCommerceDBContext.Users.Remove(user);
        }

        public async Task Save()
        {
            await eCommerceDBContext.SaveChangesAsync();
        }
    }
}
