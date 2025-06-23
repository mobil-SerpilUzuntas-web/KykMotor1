using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class SubbirCategori
    {

        /*[Key]*/
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Category Category { get; }
        public int CategoryId { get; set; }
        public List<Product> Products { get; set; }
        public List<SubIkiCategory> SubIkiCategories { get; set; }
    }

}


