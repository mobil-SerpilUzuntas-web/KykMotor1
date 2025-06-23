using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Notification
    {
        //[Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // İlişki ekleyin
        public int? BayiBasvuruId { get; set; }
        public BayiBasvuru BayiBasvuru { get; set; }
    }
}
