using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Model
{
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }

        public int productId { get; set; }
        [ForeignKey("productId")]
        public Product product { get; set; }

        public int cartId { get; set; }
        [ForeignKey("cartId")]
        public Cart cart { get; set; }

        [Column("Number of product")]
        public int number { get; set; }
    }
}
