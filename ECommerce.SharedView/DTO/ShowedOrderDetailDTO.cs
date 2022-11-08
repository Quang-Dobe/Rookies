namespace ECommerce.SharedView.DTO
{
    public class ShowedOrderDetailDTO
    {
        public int Id { get; set; }

        public ShowedProductDTO showedProductDTO { get; set; }

        public DateTime DatePurchase { get; set; }

        public int number { get; set; }

        public string comment { get; set; }

        public int rating { get; set; }

        public bool isReviewed { get; set; }
    }
}
