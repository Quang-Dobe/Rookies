using ECommerce.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductImg { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        [Column("Product's name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public ProductType ProductType { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Please fill in the required information")]
        public int Quantity { get; set; } = 50;

        public int InventoryNumber { get; set; } = 50;

        public double Rating { get; set; } = 4;


        public DateTime createdDate { get; set; } = DateTime.Now;
        public DateTime updatedDate { get; set; } = DateTime.Now;
    }
}
