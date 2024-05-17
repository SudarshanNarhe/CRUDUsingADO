using System.ComponentModel.DataAnnotations;

namespace CRUDUsingADO.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? BookName { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
