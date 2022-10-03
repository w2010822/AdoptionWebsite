using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptionWebsite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AdoptionWebsite
{
    public class CommTool
    {
        public List<SelectListItem> GetSelectLiist(string[,] itemlist, bool FirstOneEmpty = true)
        {
            List<SelectListItem> ts = new List<SelectListItem>();
            if (itemlist.Length > 0)
            {
                if (FirstOneEmpty)
                {
                    ts.Add(new SelectListItem
                    {
                        Text = "請選擇",
                        Value = ""
                    });
                }

                for (int i = 0; i < itemlist.GetLength(0); i++)
                {
                    ts.Add(new SelectListItem
                    {
                        Text = itemlist[i, 0].ToString(),
                        Value = itemlist[i, 1].ToString()
                    });
                }
            }
            return ts;
        }

    }
}
