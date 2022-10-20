using Microsoft.AspNetCore.Identity;
namespace ECommerce.SharedView.DTO
{
    public class ReviewProductDTO
    {
        public int id { get; set; }

        public IdentityUser userID { get; set; }

        public int productId { get; set; }

        public string comment { get; set; }

        public double rating { get; set; }
    }
}
