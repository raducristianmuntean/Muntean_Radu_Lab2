using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Muntean_Radu_Lab2.Data;
using Muntean_Radu_Lab2.Models;

namespace Muntean_Radu_Lab2.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly Muntean_Radu_Lab2Context _context;

        public DetailsModel(Muntean_Radu_Lab2Context context) => _context = context;

        public Author Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Author = await _context.Set<Author>().FindAsync(id);
            return Author == null ? NotFound() : Page();
        }
    }
}