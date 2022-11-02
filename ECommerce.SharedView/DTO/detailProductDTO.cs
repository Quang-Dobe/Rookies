namespace ECommerce.SharedView.DTO
{
    public class detailProductDTO
    {
        public int id { get; set; }
        public string productImg { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public int price { get; set; }
        public int inventoryNumber { get; set; } = 0;
        public double rating { get; set; }

        public List<ReviewDTO> reviewProductDTOs { get; set; }
    }
}
