using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class ProductDetayModel
    {
        public Product Product { get; set; }
        public List<Resim> Resims { get; set; }
        public Category Category { get; set; }
        public SubbirCategori SubbirCategori { get; set; }
        public SubIkiCategory subIkiCategory { get; set; }

    }
}
