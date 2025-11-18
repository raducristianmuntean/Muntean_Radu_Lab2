using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muntean_Radu_Lab2.Data;
using Muntean_Radu_Lab2.Models;

namespace Muntean_Radu_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Muntean_Radu_Lab2Context _context;

        public IndexModel(Muntean_Radu_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString;

            var books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            BookD.Books = books; // <-- FIX: Initialize BookD.Books with the loaded books

            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s =>
                    // Guard access to Author properties to avoid NullReferenceException
                    (s.Author != null && (
                        (!string.IsNullOrEmpty(s.Author.FirstName) && s.Author.FirstName.Contains(searchString)) ||
                        (!string.IsNullOrEmpty(s.Author.LastName) && s.Author.LastName.Contains(searchString))
                    )) ||
                    (!string.IsNullOrEmpty(s.Title) && s.Title.Contains(searchString))
                );
            }

            if (id != null)
            {
                BookID = id.Value;
                Book book = books
                    .Where(i => i.ID == id.Value)
                    .Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title);
                    break;
                case "author_desc":
                    // Use a safe key selector that tolerates null Author
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author != null ? s.Author.FullName : "");
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author != null ? s.Author.FullName : "");
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title);
                    break;
            }
        }
    }
}