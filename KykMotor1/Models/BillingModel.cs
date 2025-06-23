namespace KykMotorApp.WebIU.Models
{
    public class BillingModel
    {
        public int BillingId { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int BuyerId { get; set; }
        public string UserId { get; set; }
    }
}
