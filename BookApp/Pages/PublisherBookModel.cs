using BookApp.Data;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Pages
{
    public class PublisherBookModel : PageModel
    {
        public SelectList PublisherSL { get; set; }
        public SelectList AuthorSL { get; set; }
        public SelectList GenreSL { get; set; }
        public void PopulatePublisherDropDownList(BookAppContext _context,
            object selectPublisher = null)
        {
            var publisherQuery = from d in _context.Publishers
                                 orderby d.PublisherName  
                                 select d;

            PublisherSL = new SelectList(publisherQuery.AsNoTracking(),
                        "ID", "PublisherName", selectPublisher);
        }

        public void PopulateAuthorDropDownList(BookAppContext _context,
            object selectAuthor = null)
        {
            var authorQuery = from d in _context.Authors
                              orderby d.LastName  
                              select d;

             
            AuthorSL = new SelectList((from d in _context.Authors.ToList()
                                       select new
                                       {
                                           ID = d.ID,
                                           FullName = d.FirstName + " "+d.MiddleName+" " + d.LastName
                                       }), "ID", "FullName",selectAuthor);
                               
        }
        public void PopulateGenreDropDownList(BookAppContext _context,object selectGenre = null)
        {
            var genreQuery = from d in _context.Genres
                             orderby d.GenreName
                             select d;
            GenreSL = new SelectList(genreQuery.AsNoTracking(), "ID", "GenreName", selectGenre);
        }
    }
}
