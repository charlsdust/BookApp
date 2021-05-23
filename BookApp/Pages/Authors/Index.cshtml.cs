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

namespace BookApp.Pages.Authors
{
    public class IndexModel : AuthorIndexModel
    {
        private readonly BookApp.Data.BookAppContext _context;
        private readonly IConfiguration Configuration;
        public IndexModel(BookApp.Data.BookAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<AuthorIndexModel> Authors { get; set; }
        public string AuthorFullNameSort { get; set; }
        public string BooksCountSort { get; set; }
        public string GenresCountSort { get; set; }
        public string PublishersCountSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            AuthorFullNameSort = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            BooksCountSort = sortOrder == "BooksCount" ? "books_desc" : "BooksCount";
            GenresCountSort = sortOrder == "GenresCount" ? "genres_desc" : "GenresCount";
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
            IQueryable<AuthorIndexModel> AuthorsIQ =
                from b in _context.Books
                group new { b.Publisher, b.Author, b.Genre, b } by new { b.Author.ID, b.Author.FirstName, b.Author.MiddleName, b.Author.LastName } into grp
                select new AuthorIndexModel()
                {
                    AuthorFullName = grp.Key.FirstName + " " + grp.Key.MiddleName + " " + grp.Key.LastName,
                    BooksCount = grp.Select(b => b.b.ID).Distinct().Count(),
                    GenresCount = grp.Select(a => a.Genre.ID).Distinct().Count(),
                    PublishersCount = grp.Select(p => p.Publisher.ID).Distinct().Count(),
                    ID = grp.Key.ID
                };
            if (!String.IsNullOrEmpty(searchString))
            {
                AuthorsIQ = AuthorsIQ.Where(a => a.AuthorFullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "author_desc":
                    AuthorsIQ = AuthorsIQ.OrderByDescending(a => a.AuthorFullName);
                    break;
                case "books_desc":
                    AuthorsIQ = AuthorsIQ.OrderByDescending(b => b.BooksCount);
                    break;
                case "BooksCount":
                    AuthorsIQ = AuthorsIQ.OrderBy(b => b.BooksCount);
                    break;
                case "genres_desc":
                    AuthorsIQ = AuthorsIQ.OrderByDescending(g => g.GenresCount);
                    break;
                case "GenresCount":
                    AuthorsIQ = AuthorsIQ.OrderBy(g => g.GenresCount);
                    break;
                case "publishers_desc":
                    AuthorsIQ = AuthorsIQ.OrderByDescending(p => p.PublishersCount);
                    break;
                case "PublishersCount":
                    AuthorsIQ = AuthorsIQ.OrderBy(p => p.PublishersCount);
                    break;
                default:
                    AuthorsIQ = AuthorsIQ.OrderBy(a => a.AuthorFullName);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Authors = await PaginatedList<AuthorIndexModel>.CreateAsync(
                AuthorsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

    }
}

