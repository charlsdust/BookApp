using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.ViewModels
{
    public class GenreIndexModel:PageModel
    {
        [Display(Name = "Genre Name")]
        public string GenreName { get; set; }
        public int ID { get; set; }
        [Display(Name = "Books Count")]
        public int BooksCount { get; set; }
        [Display(Name ="Authors Count")]
        public int AuthorsCount { get; set; }
        [Display(Name = "Publishers Count")]
        public int PublishersCount { get; set; }
    }
}
