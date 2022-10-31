using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data.Model;

public class Order
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public IdentityUser User { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }

    public DateTime DateOfPurchase { get; set; }

    public double Total { get; set; }
}
