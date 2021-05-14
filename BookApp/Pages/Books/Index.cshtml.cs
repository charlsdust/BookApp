using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Pages.Books
{
    public class IndexModel : IndexDisplayModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public IndexModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public IList<IndexDisplayModel> Books { get;set; }
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string PublisherSort { get; set; }
        public string GenreSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
            PublisherSort = sortOrder == "Publisher" ? "publisher_desc" : "Publisher";
            GenreSort = sortOrder == "Genre" ? "genre_desc" : "Genre";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IQueryable<IndexDisplayModel> BookIQ = _context.Books
                  .Select(p => new IndexDisplayModel
                  {
                      ID=p.ID,
                      Title=p.Title,
                      AuthorFullName=p.Author.FirstName+" "+p.Author.MiddleName+" "+p.Author.LastName,
                      PublisherName=p.Publisher.PublisherName,
                      Genre=p.Genre.GenreName,
                      ISBN=p.ISBN,
                      DatePublished=p.DatePublished,
                      
                  });
            switch(sortOrder)
            {
                case "title_desc":
                    BookIQ = BookIQ.OrderByDescending(b => b.Title);
                        break;
                case "publisher_desc":
                    BookIQ = BookIQ.OrderByDescending(b => b.PublisherName);
                    break;
                case "Publisher":
                    BookIQ = BookIQ.OrderBy(b => b.PublisherName);
                    break;
                case "genre_desc":
                    BookIQ = BookIQ.OrderByDescending(b => b.Genre);
                    break;
                case "Genre":
                    BookIQ = BookIQ.OrderBy(b => b.Genre);
                    break;
                case "date_desc":
                    BookIQ = BookIQ.OrderByDescending(b => b.DatePublished);
                    break;
                case "Date":
                    BookIQ = BookIQ.OrderBy(b => b.DatePublished);
                    break;
                case "author_desc":
                    BookIQ = BookIQ.OrderByDescending(b => b.AuthorFullName);
                    break;
                case "Author":
                    BookIQ = BookIQ.OrderBy(b => b.AuthorFullName);
                    break;
                default:
                    BookIQ = BookIQ.OrderBy(b => b.Title);
                    break;


            }
            Books = await BookIQ.AsNoTracking().ToListAsync();
        }
    }
}
