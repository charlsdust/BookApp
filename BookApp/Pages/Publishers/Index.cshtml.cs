using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;
using Microsoft.Extensions.Configuration;
using BookApp.ViewModels;

namespace BookApp.Pages.Publishers
{
    public class IndexModel : PublishersIndexModel
    {
        private readonly BookApp.Data.BookAppContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(BookApp.Data.BookAppContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<PublishersIndexModel> Publishers { get;set; }
        public string PublishersNameSort { get; set; }
        public string BooksCountSort { get; set; }
        public string AuthorsCountSort { get; set; }
        public string GenresCountSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            PublishersNameSort = String.IsNullOrEmpty(sortOrder) ? "publishers_desc" : "";
            BooksCountSort = sortOrder == "BooksCount" ? "books_desc" : "BooksCount";
            AuthorsCountSort = sortOrder == "AuthorsCount" ? "authors_desc" : "AuthorsCount";
            GenresCountSort = sortOrder == "GenresCount" ? "genres_desc" : "GenresCount";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<PublishersIndexModel> PublishersIQ =
                from b in _context.Books
                group new { b.Genre, b, b.Author, b.Publisher } by new { b.Publisher.ID, b.Publisher.PublisherName } into grp
                select new PublishersIndexModel
                {
                    PublisherName = grp.Key.PublisherName,
                    ID = grp.Key.ID,
                    BooksCount = grp.Select(b => b.b.ID).Distinct().Count(),
                    AuthorsCount=grp.Select(a=>a.Author.ID).Distinct().Count(),
                    GenresCount=grp.Select(g=>g.Genre.ID).Distinct().Count()

                };
            if (!String.IsNullOrEmpty(searchString))
            {
                PublishersIQ = PublishersIQ.Where(p => p.PublisherName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "publishers_desc":
                    PublishersIQ = PublishersIQ.OrderByDescending(p => p.PublisherName);
                    break;
                case "books_desc":
                    PublishersIQ = PublishersIQ.OrderByDescending(b => b.BooksCount);
                    break;
                case "BooksCount":
                    PublishersIQ = PublishersIQ.OrderBy(b => b.BooksCount);
                    break;
                case "authors_desc":
                    PublishersIQ = PublishersIQ.OrderByDescending(a => a.AuthorsCount);
                    break;
                case "AuthorsCount":
                    PublishersIQ = PublishersIQ.OrderBy(a => a.AuthorsCount);
                    break;
                case "genres_desc":
                    PublishersIQ = PublishersIQ.OrderByDescending(p => p.GenresCount);
                    break;
                case "GenresCount":
                    PublishersIQ = PublishersIQ.OrderBy(p => p.GenresCount);
                    break;
                default:
                    PublishersIQ = PublishersIQ.OrderBy(a => a.PublisherName);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Publishers = await PaginatedList<PublishersIndexModel>.CreateAsync(
                PublishersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
