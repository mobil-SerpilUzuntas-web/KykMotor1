using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class ProductDetayViewModel
    {
        
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int KoliAdet { get; set; }
        public List<Resim> Resims { get; set; }
        public List<Category> Categories { get; set; } 
        public int SubIkiCategoryId { get; set; }
   
    }
}
