using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "Genre Name")]
        public string GenreName { get; set; }
        [Display(Name = "Books")]
        public ICollection<Book> books { get; set; }
    }
}
