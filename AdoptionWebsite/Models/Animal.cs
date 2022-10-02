using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
