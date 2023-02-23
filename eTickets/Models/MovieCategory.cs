using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class MovieCategory
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
