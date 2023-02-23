using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name {get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        //public string ImageURL { get; set; }
        public DateTime ReleaseStartDate { get; set; }
        [Required]
        public DateTime ReleaseEndDate { get; set; }
        [ForeignKey("MovieCategoryId")]
        public int MovieCategoryId { get; set; }
        public virtual MovieCategory MovieCategory { get; set; }
        //Relationships
         public virtual ICollection<Actor_Movie> Actors_Movies { get; set; }
        //Relationships
        public virtual ICollection<Movie_Cinema> Movies_Cinemas { get; set; }

        //public virtual Cinema Cinema { get; set; }

        //Producer
        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
