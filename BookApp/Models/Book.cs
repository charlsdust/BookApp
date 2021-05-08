using System;
using System.Collections.Generic;
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
        public DateTime DatePublished { get; set; }
        public int PublisherID { get; set; }
        public Publisher Publisher { get; set; }
    }
}
