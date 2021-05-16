using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Author
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }
       
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Middle name cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
