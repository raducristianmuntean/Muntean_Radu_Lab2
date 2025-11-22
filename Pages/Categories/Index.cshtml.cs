using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muntean_Radu_Lab2.Data;
using Muntean_Radu_Lab2.Models;

namespace Muntean_Radu_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Muntean_Radu_Lab2Context _context;

        public IndexModel(Muntean_Radu_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        // New view model with books + categories
        public BookData BookD { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            BookD = new BookData();

            // Load categories (including BookCategories -> Book -> Author for convenience)
            Category = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .AsNoTracking()
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            // Load all books with authors and their BookCategories
            var books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .AsNoTracking()
                .ToListAsync();

            BookD.Categories = Category;
            BookD.Books = books;

            if (id != null)
            {
                CategoryID = id.Value;
                // Filter books that belong to the selected category (guard nulls)
                BookD.Books = BookD.Books
                    .Where(b => b.BookCategories != null && b.BookCategories.Any(bc => bc.CategoryID == CategoryID))
                    .ToList();
            }
        }
    }
}
