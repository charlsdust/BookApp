using BookApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Pages
{
    public class DetailsDisplayModel:PageModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Total Pages")]
        public int TotalPages { get; set; }
        public byte Rating { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name="Date Published")]
        public DateTime DatePublished { get; set; }
        [Display(Name ="Author Name")]
        public string AuthorName { get; set; }
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
        [Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }
        

    }
}
