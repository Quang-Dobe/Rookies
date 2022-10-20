using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.SharedView.DTO
{
    public class ProductDTO
    {
        public string productImg { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        [Column("Product's name")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string productType { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public int price { get; set; }
    }
}
