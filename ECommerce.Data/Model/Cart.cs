using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Model
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string userId { get; set; }
        [ForeignKey("userId")]
        public IdentityUser user { get; set; }

        public List<CartDetail> cartDetails { get; set; }

        public double total { get; set; }
    }
}
