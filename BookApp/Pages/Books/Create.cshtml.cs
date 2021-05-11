using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Pages.Books
{
    public class CreateModel : PublisherBookModel
    {
        private readonly BookApp.Data.BookAppContext _context;
         

        public CreateModel(BookApp.Data.BookAppContext context)
        {
            
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulatePublisherDropDownList(_context);
            PopulateAuthorDropDownList(_context);
            PopulateGenreDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyBook = new Book();
            if (await TryUpdateModelAsync<Book>(
                emptyBook,
                "book",
               s=>s.Title,s=>s.TotalPages,s=>s.Rating,s => s.ISBN,s => s.DatePublished,s => s.PublisherID,s => s.AuthorID,s => s.GenreID))
            {
                _context.Books.Add(emptyBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulatePublisherDropDownList(_context, emptyBook.PublisherID);
            PopulateAuthorDropDownList(_context, emptyBook.AuthorID);
            PopulateGenreDropDownList(_context, emptyBook.GenreID);
            return Page();
        }
    }
}
