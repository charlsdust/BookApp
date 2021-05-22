using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public DetailsModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public Author Author { get; set; }
        public IList<Genre> Genres { get; set; }
        public IList<Publisher> Publishers { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await _context.Authors.Include(b=>b.Books).FirstOrDefaultAsync(m => m.ID == id);
            var GenreIQ = from b in _context.Books
                          where b.AuthorID == id
                          select new Genre()
                          {
                              GenreName = b.Genre.GenreName,
                              ID = b.Genre.ID
                          };
            Genres = await GenreIQ.Distinct().ToListAsync();
            var PublishersIQ = from b in _context.Books
                               where b.AuthorID == id
                               select new Publisher()
                               {
                                   PublisherName = b.Publisher.PublisherName,
                                   ID = b.Publisher.ID
                               };
            Publishers = await PublishersIQ.Distinct().ToListAsync();


            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
