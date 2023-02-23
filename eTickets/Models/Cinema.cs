using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema
    {
      
        [Key]
        public int Id { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        //Relationships
        public virtual ICollection<Movie_Cinema> Movies_Cinemas { get; set; }
        //Relationships
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
