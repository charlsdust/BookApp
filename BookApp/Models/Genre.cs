using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string GenreName { get; set; }
        public ICollection<Book> books { get; set; }
    }
}
