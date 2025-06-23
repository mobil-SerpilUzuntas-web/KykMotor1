using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubbirCategori> SubbirCategories { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
