using Microsoft.AspNetCore.Identity;
namespace ECommerce.SharedView.DTO
{
    public class ReviewProductDTO
    {
        public string userName { get; set; }

        public string comment { get; set; }

        public int rating { get; set; }

    }
}
