using KykShopApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace KykMotorApp.WebIU.Models
{
    public class ProductModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Koli Adet is required")]
        public int KoliAdet { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        public int? SubBirId { get; set; }
        public int? SubIkiId { get; set; }

        public string Description { get; set; }

        public List<Category> Categories { get; set; }
        public List<SubbirCategori> SubBirCategories { get; set; }
        public List<SubIkiCategory> SubIkiCategories { get; set; }
    }

    //public class ProductModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }
    //    public string Description { get; set; }
    //    public int KoliAdet { get; set; }
    //    public int CategoryId { get; set; }
    //    public int SubBirId { get; set; }
    //    public int SubIkiId { get; set; }
    //    // Drop-down için gerekli veriler
    //    public List<Category> Categories { get; set; }
    //    public List<SubbirCategori> SubBirCategories { get; set; }
    //    public List<SubIkiCategory> SubIkiCategories { get; set; }
    //}
}

