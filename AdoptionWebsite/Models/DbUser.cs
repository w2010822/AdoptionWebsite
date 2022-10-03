using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AdoptionWebsite.Models
{
    public partial class DbUser
    {
        public int Id { get; set; }
        [Display(Name ="帳號")]
        public string UserName { get; set; }
        [Display(Name = "密碼")]
        public string UserPassword { get; set; }
    }
}
