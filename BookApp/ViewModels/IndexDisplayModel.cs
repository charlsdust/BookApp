using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Pages
{
    public class IndexDisplayModel:PageModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [Display(Name ="Author Name")]
        public string AuthorFullName { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Date Published")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }
        [Display(Name ="Publisher Name")]
        public string PublisherName { get; set; }
    }
}
