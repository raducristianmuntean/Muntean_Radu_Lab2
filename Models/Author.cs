using System.ComponentModel.DataAnnotations;

namespace Muntean_Radu_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Optional: dacă vrei relație one-to-many
        public ICollection<Book>? Books { get; set; }
    }
}
