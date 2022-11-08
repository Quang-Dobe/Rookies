using ECommerce.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data.Model
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Column("Number of product")]
        public int Number { get; set; }

        public DateTime DatePurchase { get; set; }

        public string Comment { get; set; }

        public ProductRating Rating { get; set; }

        public bool IsReviewed { get; set; } = false;
    }
}
