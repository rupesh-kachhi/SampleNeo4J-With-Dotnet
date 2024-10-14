namespace SampleNeo4J.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Activity { get; set; }
    }

}
