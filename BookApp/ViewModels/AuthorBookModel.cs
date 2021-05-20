using BookApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Pages
{
    public class AuthorBookModel:PageModel
    {
        public SelectList AuthorSL { get; set; }
        public void PopulateAuthorDropDownList(BookAppContext _context,
            object selectAuthor = null)
        {
            var authorQuery = from d in _context.Authors
                                 orderby d.LastName // Sort by name.
                                 select d;

            AuthorSL = new SelectList(authorQuery.AsNoTracking(),
                        "ID", "FirstName"+" "+"MiddleName"+" "+"LastName", selectAuthor);

        }
    }
}
