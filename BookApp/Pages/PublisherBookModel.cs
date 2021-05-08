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
    public class PublisherBookModel:PageModel
    {
        public SelectList PublisherSL { get; set; }

        public void PopulateKoloryDropDownList(BookAppContext _context,
            object selectPublisher = null)
        {
            var publisherQuery = from d in _context.Publishers
                             orderby d.Name // Sort by name.
                             select d;

            PublisherSL = new SelectList(publisherQuery.AsNoTracking(),
                        "ID", "Name", selectPublisher);
        }
    }
}
