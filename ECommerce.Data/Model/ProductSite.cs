using System.ComponentModel.DataAnnotations;

namespace ECommerce.Data.Model
{
    public class ProductSite
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string CompanyName { get; set; }

        public List<Product> Products { get; set; }
    }
}
