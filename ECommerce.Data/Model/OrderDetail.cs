using ECommerce.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data.Model
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int productId { get; set; }
        [ForeignKey("productId")]
        public Product product { get; set; }

        public int orderId { get; set; }
        [ForeignKey("orderId")]
        public Order order { get; set; }

        [Column("Number of product")]
        public int number { get; set; }

        public string comment { get; set; }

        public ProductRating rating { get; set; }
    }
}
