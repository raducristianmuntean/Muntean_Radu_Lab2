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
    public class EditModel : PageModel
    {
        private readonly Muntean_Radu_Lab2.Data.Muntean_Radu_Lab2Context _context;

        public EditModel(Muntean_Radu_Lab2.Data.Muntean_Radu_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

            var authorsList = await _context.Set<Author>()
                .Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName })
                .ToListAsync();
            ViewData["AuthorID"] = new SelectList(authorsList, "ID", "FullName", Book.AuthorID);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Rebuild selects just like OnGet so the page can re-render with dropdowns
                ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

                var authorsList = await _context.Set<Author>()
                    .Select(a => new { a.ID, FullName = a.FirstName + " " + a.LastName })
                    .ToListAsync();
                ViewData["AuthorID"] = new SelectList(authorsList, "ID", "FullName", Book.AuthorID);

                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
