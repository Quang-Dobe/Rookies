using ECommerce.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.BackendAPI.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrder();
        Task<Order> GetOrder(int id);
        Task<Order> GetOrder(IdentityUser user);

    }
}
