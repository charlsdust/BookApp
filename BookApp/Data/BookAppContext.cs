using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookApp.Models;

namespace BookApp.Data
{
    public class BookAppContext : DbContext
    {
        public BookAppContext (DbContextOptions<BookAppContext> options)
            : base(options)
        {
        }

        public DbSet<BookApp.Models.Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Publisher>().ToTable("Publisher");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Genre>().ToTable("Genre");

        }
    }
}
