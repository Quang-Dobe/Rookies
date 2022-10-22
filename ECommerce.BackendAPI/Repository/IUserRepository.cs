using Microsoft.AspNetCore.Identity;

namespace ECommerce.BackendAPI.Repository
{
    public interface IUserRepository
    {
        Task<List<IdentityUser>> GetUsers();
        Task<IdentityUser> GetUserByName(string userName);
        Task<IdentityUser> GetUserByEmail(string email);
        Task CreateUser(IdentityUser identityUser);
        void UpdateUser(IdentityUser identityUser);
        Task DeleteUserByName(IdentityUser user);
        Task Save();
    }
}
