using System;
using System.Collections.Generic;

namespace AdoptionWebsite.Models
{
    public partial class Files
    {
        public int Idno { get; set; }
        public string XTable { get; set; }
        public int TableId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? isDel { get; set; }
    }
}
