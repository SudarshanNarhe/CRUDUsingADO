using System.ComponentModel.DataAnnotations;

namespace CRUDUsingADO.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public double Java { get; set; }

        [Required]
        public double DotNet { get; set; }

        [Required]
        public double SQL { get; set; }

        [Required]
        public double Angular { get; set; }
        public double Percentage { get; set; }
    }
}
