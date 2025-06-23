using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{

    public class ProductListModel
    {
       
        public List<Product> Products { get; set; }
        public List<Resim> Resims { get; set; }
        public List<Category> Categories { get; set; }
        public int? CategoryId { get; set; }
        public List<SubbirCategori> SubbirCategori { get;set; }
        public int? SubBirId { get; set; }
        public List<SubIkiCategory> SubIkiCategory { get; set;}
        public int? SubIkiId { get; set; }
    }

}

