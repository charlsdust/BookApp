using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Pages.Genres
{
    public class DetailsModel : PageModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public DetailsModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public Genre Genre { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Publisher> Publishers { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre = await _context.Genres.Include(b => b.books).FirstOrDefaultAsync(m => m.ID == id);
            var AuthorIQ = from b in _context.Books
                           where b.GenreID == id
                           select new Author
                           {
                               FirstName = b.Author.FirstName,
                               LastName = b.Author.LastName,
                               ID = b.Author.ID
                           };
            Authors = await AuthorIQ.Distinct().ToListAsync();
            var PublishersIQ = from b in _context.Books
                               where b.GenreID == id
                               select new Publisher
                               {
                                   PublisherName = b.Publisher.PublisherName,
                                   ID = b.Publisher.ID
                               };
            Publishers = await PublishersIQ.Distinct().ToListAsync();
            if (Genre == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
