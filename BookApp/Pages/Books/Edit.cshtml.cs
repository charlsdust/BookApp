using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;
using BookApp.Models;

namespace BookApp.Pages.Books
{
    public class EditModel : PublisherBookModel
    {
        private readonly BookApp.Data.BookAppContext _context;

        public EditModel(BookApp.Data.BookAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books
                .Include(b => b.Publisher).FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
            PopulatePublisherDropDownList(_context,Book.PublisherID);
            PopulateAuthorDropDownList(_context, Book.AuthorID);
            PopulateGenreDropDownList(_context, Book.GenreID);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Books.FindAsync(id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                 bookToUpdate,
                 "book",   // Prefix for form value.
                   s => s.Title, s => s.TotalPages, s => s.Rating, s => s.ISBN,
                   s => s.DatePublished, s => s.PublisherID,s => s.AuthorID,s =>s.GenreID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

             
            PopulatePublisherDropDownList(_context, bookToUpdate.PublisherID);
            PopulateAuthorDropDownList(_context, bookToUpdate.AuthorID);
            PopulateGenreDropDownList(_context, bookToUpdate.GenreID);
            return Page();
        }

        //private bool BookExists(int id)
        //{
        //    return _context.Books.Any(e => e.ID == id);
        //}
    }
}
