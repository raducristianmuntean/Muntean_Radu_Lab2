using System.Collections.Generic;
using System.Linq;
using Muntean_Radu_Lab2.Models;

namespace Muntean_Radu_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } = Enumerable.Empty<Publisher>();
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
    }
}
