using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Pages
{
    public class IndexDisplayModel:PageModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TotalPages { get; set; }
        public byte Rating { get; set; }
        public string ISBN { get; set; }
        public DateTime DatePublished { get; set; }
        public string Name { get; set; }
    }
}
