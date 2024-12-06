namespace CRM_Project.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int? ByuerId { get; set; }
        public int SellerId { get; set; }
        public int CultureId { get; set; }
        public double OrderAmount { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
