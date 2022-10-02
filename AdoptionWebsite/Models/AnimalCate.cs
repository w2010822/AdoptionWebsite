﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdoptionWebsite.Models
{
    public partial class AnimalCate
    {
        public int Idno { get; set; }
        [Display(Name = "動物類別")]
        public string CateName { get; set; }
    }
}
