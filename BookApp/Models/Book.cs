using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Book
    {
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Total pages")]
        public int TotalPages { get; set; }
        [Range(0, 5)]
        public byte Rating { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date Published")]
        public DateTime DatePublished { get; set; }
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

    }
}
