﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AdoptionWebsite.Models
{
    public partial class Files
    {
        public int Idno { get; set; }
        public string XTable { get; set; }
        public int? TableId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? IsDel { get; set; }
    }
}
