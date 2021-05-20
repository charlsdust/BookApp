﻿using System;
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
            IQueryable<AuthorIndexModel> AuthorsIQ =
                from author in _context.Authors
                
                select new AuthorIndexModel()
                { AuthorFullName=author.FirstName+" "+author.MiddleName+" "+author.LastName,
                BooksCount=author.Books.Count(),
                ID=author.ID
                };
            Authors = await AuthorsIQ.ToListAsync();
        }
    }
}
