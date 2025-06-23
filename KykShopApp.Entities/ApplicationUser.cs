using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class ApplicationUser :IdentityUser
    {

        public string Name { get; set; }
        public string SurName { get; set; }
        public string Ctiy { get; set; }
        public string FirmaAdı { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNo { get; set; }
        public bool IsActive { get; set; } = true;
      
    
 
    }
}
