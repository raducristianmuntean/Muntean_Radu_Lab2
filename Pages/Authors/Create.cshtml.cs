using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muntean_Radu_Lab2.Data;
using Muntean_Radu_Lab2.Models;

namespace Muntean_Radu_Lab2.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly Muntean_Radu_Lab2Context _context;

        public CreateModel(Muntean_Radu_Lab2Context context) => _context = context;

        [BindProperty]
        public Author Author { get; set; } = default!;

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Set<Author>().Add(Author);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}