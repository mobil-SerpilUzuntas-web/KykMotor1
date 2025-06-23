using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class CategoryListModel
    {
        public List<Category> Categories { get; set; }
        public int CategoryId { get; set; }
        public List<SubbirCategori> SubbirCategoris { get; set; } // Boş liste atandı
        public int SubirId { get; set; }
        public List<SubIkiCategory> SubIkiCategories { get; set; } // Boş liste atandı
   

    }


}




