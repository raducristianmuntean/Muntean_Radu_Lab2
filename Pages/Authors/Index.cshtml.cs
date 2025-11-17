using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muntean_Radu_Lab2.Data;
using Muntean_Radu_Lab2.Models;

namespace Muntean_Radu_Lab2.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly Muntean_Radu_Lab2Context _context;

        public IndexModel(Muntean_Radu_Lab2Context context) => _context = context;

        public IList<Author> Author { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Author = await _context.Set<Author>().AsNoTracking().ToListAsync();
        }
    }
}