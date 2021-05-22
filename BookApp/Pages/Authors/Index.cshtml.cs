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

namespace BookApp.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public IndexModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public IList<AuthorIndexModel> Authors { get;set; }

        public async Task OnGetAsync()
        {
            /*IQueryable<AuthorIndexModel> AuthorsIQ =
                from author in _context.Authors
                
                select new AuthorIndexModel()
                { AuthorFullName=author.FirstName+" "+author.MiddleName+" "+author.LastName,
                BooksCount=author.Books.Count(),
                ID=author.ID
                };*/
            IQueryable<AuthorIndexModel> AuthorsIQ =
                from b in _context.Books
                group new { b.Publisher, b.Author, b.Genre, b } by new { b.Author.ID, b.Author.FirstName, b.Author.MiddleName, b.Author.LastName } into grp
                select new AuthorIndexModel()
                {
                    AuthorFullName = grp.Key.FirstName + " " + grp.Key.MiddleName + " " + grp.Key.LastName,
                    BooksCount = grp.Select(b => b.b.ID).Distinct().Count(),
                    GenresCount = grp.Select(a => a.Genre.ID).Distinct().Count(),
                    PublishersCount = grp.Select(p => p.Publisher.ID).Distinct().Count(),
                    ID=grp.Key.ID
                };
                
            Authors = await AuthorsIQ.ToListAsync();
        }
    }
}
