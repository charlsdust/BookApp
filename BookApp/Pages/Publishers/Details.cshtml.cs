using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public DetailsModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public Publisher Publisher { get; set; }
        public IList<Genre> Genres { get; set; }
        public IList<Author> Authors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher = await _context.Publishers.Include(b=>b.Books).FirstOrDefaultAsync(m => m.ID == id);
            var GenreIQ = from b in _context.Books
                          where b.PublisherID == id
                          select new Genre()
                          {
                              GenreName = b.Genre.GenreName,
                              ID = b.Genre.ID
                          };
            Genres = await GenreIQ.Distinct().ToListAsync();
            var AuthorsIQ = from b in _context.Books
                            where b.PublisherID == id
                            select new Author()
                            {
                                FirstName = b.Author.FirstName,
                                LastName = b.Author.LastName,
                                ID = b.Author.ID
                            };
            Authors = await AuthorsIQ.Distinct().ToListAsync();
            if (Publisher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
