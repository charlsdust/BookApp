using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;
using BookApp.ViewModels;

namespace BookApp.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public IndexModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public IList<GenreIndexModel> Genres { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<GenreIndexModel> GenredIQ =
                from b in _context.Books
                group new { b.Genre, b, b.Author } by new { b.Genre.ID, b.Genre.GenreName } into groping
                select new GenreIndexModel()
                {
                    ID = groping.Key.ID,
                    GenreName = groping.Key.GenreName,

                    BooksCount = groping.Select(b => b.b.ID).Distinct().Count(),
                    AuthorsCount = groping.Select(a => a.Author.ID).Distinct().Count()
                };
            Genres = await GenredIQ.ToListAsync();

        }
    }
}
