using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Contentt { get; set; }
        public Nullable<System.DateTime> DateofAdded { get; set; }
        public Nullable<bool> Confirmation { get; set; }
        public Nullable<bool> Home { get; set; }
        public Nullable<int> CategoryID { get; set; }

        public  Category Category { get; set; }
    }
}