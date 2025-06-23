﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KykMotorApp.WebIU.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {

        [Required(ErrorMessage = "Role isim alanı boş bırakılamaz.")]
        [Display(Name = "Role ismi :")]
        public string Name { get; set; }
    }
}
