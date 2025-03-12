using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public required string? FullName { get; set; }
        public required string? Address { get; set; }
        public int Age { get; set; }
    }
}