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
    public class CreateModel : PageModel
    {
        private readonly Muntean_Radu_Lab2Context _context;

        public CreateModel(Muntean_Radu_Lab2Context context)
        {
            _context = context;
        }

        public SelectList AuthorList { get; set; } = default!;

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

            var authorsList = _context.Set<Author>()
                .Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName })
                .ToList();
            ViewData["AuthorID"] = new SelectList(authorsList, "ID", "FullName");

            AuthorList = new SelectList(_context.Set<Author>(), "ID", "LastName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // 🔹 Reîncărcăm dropdown-urile dacă formularul este invalid
                ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

                var authorsList = await _context.Set<Author>()
                    .Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName })
                    .ToListAsync(); 
                ViewData["AuthorID"] = new SelectList(authorsList, "ID", "FullName");

                AuthorList = new SelectList(_context.Set<Author>(), "ID", "LastName");

                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
