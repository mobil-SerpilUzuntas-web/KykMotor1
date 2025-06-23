using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class SubIkiCategory
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Product> Products { get; set; }
        public SubbirCategori SubbirCategori { get; set; }
        public int SubBirId { get; set; }
    }
}


