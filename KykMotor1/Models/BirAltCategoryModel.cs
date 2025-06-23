using KykShopApp.Entities;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KykMotorApp.WebIU.Models
{
    public class BirAltCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public int CategoryId { get; set; }
        public List<Product> Products { get; set; }
        public List<SubIkiCategory> SubIkiCategories { get; set; }
        public List<SubbirCategori> SubbirCategori { get; set; }
        public int SubBirId { get; set; }
    }
}
