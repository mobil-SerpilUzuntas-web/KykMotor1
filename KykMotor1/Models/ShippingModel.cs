﻿namespace KykMotorApp.WebIU.Models
{
    public class ShippingModel
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        // User ilişkisi için
        public string UserId { get; set; }
        public int BuyerId { get; set; }
    }
}
