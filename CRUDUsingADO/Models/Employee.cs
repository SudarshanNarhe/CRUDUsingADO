using System.ComponentModel.DataAnnotations;

namespace CRUDUsingADO.Models
{
    public class Employee
    {
        [Key] // this is PK in table
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } // ? to allow null value from DB
        [Required]
        public string? City { get; set; }
        [Required]
        public double Salary { get; set; }
    }
}
