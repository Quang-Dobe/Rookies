using ECommerce.SharedView.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.SharedView.DTO
{
    public class detailProductDTO
    {
        public int id { get; set; }
        public string productImg { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public string productType { get; set; }
        public int price { get; set; }
        public int inventoryNumber { get; set; } = 0;
        public double rating { get; set; }

        public List<ReviewProductDTO> reviewProductDTOs { get; set; }
    }
}
