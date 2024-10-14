using System.ComponentModel.DataAnnotations;

namespace SampleNeo4J.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<string> Skills { get; set; } = new List<string>(); // Initialized to avoid null reference

        [StringLength(50)]
        public string Level { get; set; }
    }
}
