using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.ViewModels
{
    public class AuthorIndexModel:PageModel
    {
        [Display(Name ="Author Name")]
        public string AuthorFullName { get; set; }
        public int ID { get; set; }
        [Display(Name = "Books Count")]
        public int BooksCount { get; set; }
        [Display(Name = "Genres Count")]
        public int GenresCount { get; set; }
        [Display(Name = "Publishers Count")]
        public int PublishersCount { get; set; }
    }
}
