using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.SharedView.DTO
{
    public class CartDTO
    {
        public string userId { get; set; }

        public double total { get; set; } = 0;
    }
}
