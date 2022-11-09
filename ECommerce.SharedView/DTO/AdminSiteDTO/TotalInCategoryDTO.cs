using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.SharedView.DTO.AdminSiteDTO
{
    public class TotalInCategoryDTO
    {
        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public double total { get; set; }
    }
}
