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
using Microsoft.Extensions.Configuration;

namespace BookApp.Pages.Genres
{
    public class IndexModel : GenreIndexModel
    {
        private readonly BookApp.Data.BookAppContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(BookApp.Data.BookAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<GenreIndexModel> Genres { get; set; }
        public string GenreNameSort { get; set; }
        public string BooksCountSort { get; set; }
        public string AuthorsCountSort { get; set; }
        public string PublishersCountSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            {
                GenreNameSort = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
                BooksCountSort = sortOrder == "BooksCount" ? "books_desc" : "BooksCount";
                AuthorsCountSort = sortOrder == "AuthorsCount" ? "authors_desc" : "AuthorsCount";
                PublishersCountSort = sortOrder == "PublishersCount" ? "publishers_desc" : "PublishersCount";
                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                CurrentFilter = searchString;
                IQueryable<GenreIndexModel> GenreIQ =
                from b in _context.Books
                group new { b.Genre, b, b.Author, b.Publisher } by new { b.Genre.ID, b.Genre.GenreName } into groping
                select new GenreIndexModel()
                {
                    ID = groping.Key.ID,
                    GenreName = groping.Key.GenreName,
                    PublishersCount = groping.Select(g => g.Publisher.ID).Distinct().Count(),
                    BooksCount = groping.Select(b => b.b.ID).Distinct().Count(),
                    AuthorsCount = groping.Select(a => a.Author.ID).Distinct().Count()

                };
                if (!String.IsNullOrEmpty(searchString))
                {
                    GenreIQ = GenreIQ.Where(g => g.GenreName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "genre_desc":
                        GenreIQ = GenreIQ.OrderByDescending(g => g.GenreName);
                        break;
                    case "books_desc":
                        GenreIQ = GenreIQ.OrderByDescending(b => b.BooksCount);
                        break;
                    case "BooksCount":
                        GenreIQ = GenreIQ.OrderBy(b => b.BooksCount);
                        break;
                    case "authors_desc":
                        GenreIQ = GenreIQ.OrderByDescending(a => a.AuthorsCount);
                        break;
                    case "AuthorsCount":
                        GenreIQ = GenreIQ.OrderBy(a => a.AuthorsCount);
                        break;
                    case "publishers_desc":
                        GenreIQ = GenreIQ.OrderByDescending(p => p.PublishersCount);
                        break;
                    case "PublishersCount":
                        GenreIQ = GenreIQ.OrderBy(p => p.PublishersCount);
                        break;
                    default:
                        GenreIQ = GenreIQ.OrderBy(a => a.GenreName);
                        break;
                }
                var pageSize = Configuration.GetValue("PageSize", 4);
                Genres = await PaginatedList<GenreIndexModel>.CreateAsync(
                    GenreIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            }
        }
    }
}
