using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Resim
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; } = true;
        public Product Product { get; set; }
    }

}
