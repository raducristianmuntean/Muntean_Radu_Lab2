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
        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();

            var books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)                 // <- include author navigation
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            BookD.Books = books;

            if (id != null)
            {
                BookID = id.Value;
                Book book = books
                    .Where(i => i.ID == id.Value)
                    .Single();
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}