using System.ComponentModel.DataAnnotations;

namespace CRUDUsingADO.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range (minimum:1,maximum:1000000)]
        public double Price { get; set; }
    }
}
