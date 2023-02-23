using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie_Cinema
    {
        [Key]
       public int Id { get; set; }

        public int? MovieId { get; set; }

        public int? CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public virtual Cinema Cinema { get; set; }
       
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
