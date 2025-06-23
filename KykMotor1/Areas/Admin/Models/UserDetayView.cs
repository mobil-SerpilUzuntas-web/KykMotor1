using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace KykMotorApp.WebIU.Areas.Admin.Models
{
    public class UserDetayView
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string City { get; set; } //
        public string Phone { get; set; }
        public string Email { get; set; }// Ctiy yerine City olarak 
        public string FirmaAdı { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
