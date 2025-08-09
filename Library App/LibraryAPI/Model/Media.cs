using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Model
{
    public class Media
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Title cannot be longer than 150 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "Author name cannot be longer than 100.")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "Genre cannot be longer than 50 characters.")]
        public string Genre { get; set; } = string.Empty;
        [Required]
        [StringLength(13, ErrorMessage = "ISBN cannot be longer than 13 characters.")]
        [RegularExpression(@"^[0-9-]{10,13}$")]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public string DateReleased { get; set; } = string.Empty;
        public bool HasCover { get; set; }
    }
}
