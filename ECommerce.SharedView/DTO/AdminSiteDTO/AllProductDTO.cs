namespace ECommerce.SharedView.DTO.AdminSiteDTO
{
    public class AllProductDTO
    {
        public int id { get; set; }

        public string ProductImg { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int InventoryNumber { get; set; }

        public double Rating { get; set; }


        public DateTime createdDate { get; set; }

        public DateTime updatedDate { get; set; }
    }
}
