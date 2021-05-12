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

        public async Task OnGetAsync()
        {
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
            Books = await BookIQ.AsNoTracking().ToListAsync();
        }
    }
}
