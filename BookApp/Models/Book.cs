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
        public string Title { get; set; }
        public int TotalPages { get; set; }
        public byte Rating { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePublished { get; set; }
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

    }
}
