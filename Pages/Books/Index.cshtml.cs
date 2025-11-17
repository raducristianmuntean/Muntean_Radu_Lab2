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

        // Lista pentru dropdown
        public SelectList Authors { get; set; }

        // Parametru de filtrare
        [BindProperty(SupportsGet = true)]
        public int? AuthorId { get; set; }

        public async Task OnGetAsync()
        {
            // Interogare cărți cu Publisher și Author
            var booksQuery = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .AsQueryable();

            // Filtrare după autor dacă AuthorId e setat
            if (AuthorId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.AuthorID == AuthorId.Value);
            }

            Book = await booksQuery.ToListAsync();

            // FIX: Use Set<Author>() instead of _context.Author
            var authorsList = await _context.Set<Author>()
                .Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName })
                .ToListAsync();

            Authors = new SelectList(authorsList, "ID", "FullName", AuthorId);
        }
    }
}
