using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data.Model;

public class Order
{
    [Key]
    public int Id { get; set; }

    public string userId { get; set; }
    [ForeignKey("userId")]
    public IdentityUser user { get; set; }

    public List<OrderDetail> orderDetails { get; set; }

    public DateTime dateOfPurchase { get; set; }

    public double total { get; set; }
}
