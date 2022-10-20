using ECommerce.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data.Model
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        public string productImg { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        [Column("Product's name")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public ProductType productType { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public int price { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public int quantity { get; set; } = 0;

        public int inventoryNumber { get; set; } = 0;

        public double rating { get; set; } = 1;

        //public ProductSite productSiteId { get; set; }


        public DateTime createdDate { get; set; } = DateTime.Now;
        public DateTime updatedDate { get; set; } = DateTime.Now;
    }
}
