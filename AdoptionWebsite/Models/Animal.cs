using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AdoptionWebsite.Models
{
    public partial class Animal
    {
        public int Idno { get; set; }
        [Display(Name = "動物類別")]
        public int CateId { get; set; }
        [Display(Name = "名稱(暱稱)")]
        public string Name { get; set; }
        [Display(Name = "說明")]
        public string Memo { get; set; }
        [Display(Name = "領養狀態")]
        public int? IsAdoption { get; set; }
        [Display(Name = "性別")]
        public string Sex { get; set; }
    }
}
