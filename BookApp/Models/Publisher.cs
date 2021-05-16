using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name ="Publisher Name")]
        public string PublisherName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}