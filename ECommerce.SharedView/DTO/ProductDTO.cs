using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerce.SharedView.Enum;

namespace ECommerce.SharedView.DTO
{
    public class ProductDTO
    {
        public string productImg { get; set; }

        public string productName { get; set; }

        public string description { get; set; }

        public ProductType productType { get; set; }

        public int price { get; set; }
    }
}
