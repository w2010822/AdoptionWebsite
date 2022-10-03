using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AdoptionWebsite.Models
{
    public partial class AnimalCate
    {
        public int Idno { get; set; }
        [Display(Name = "動物類別")]
        public string CateName { get; set; }
    }
}
