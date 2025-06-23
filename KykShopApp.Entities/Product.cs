using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Product
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int KoliAdet { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public SubbirCategori SubbirCategori { get; set; }
        public int? SubBirId { get; set; }
        public SubIkiCategory SubIkiCategory { get; set; }
        public int? SubIkiId { get; set; }
        public List<Resim> Resims { get; set; }
        public bool IsDeleted { get; set; } = true;
    }


}

