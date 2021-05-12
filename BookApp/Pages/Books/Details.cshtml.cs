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
    public class DetailsModel : DetailsDisplayModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public DetailsModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        public DetailsDisplayModel Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            Book = await _context.Books.Select(p => new DetailsDisplayModel
            {
                ID = p.ID,
                Title = p.Title,
                TotalPages = p.TotalPages,
                Rating = p.Rating,
                ISBN = p.ISBN,
                DatePublished = p.DatePublished,
                AuthorName = p.Author.FirstName+" "+ p.Author.MiddleName+" "+p.Author.LastName,
                PublisherName = p.Publisher.PublisherName,
                GenreName = p.Genre.GenreName,



            }).FirstOrDefaultAsync(p => p.ID == id);




            //Book = await _context.Books
            //    .Include(b => b.Publisher).FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}