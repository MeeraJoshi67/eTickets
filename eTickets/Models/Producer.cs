using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfilePictureURL { get; set; }
        [Required]
        public string FullName { get; set;}
        [Required]
        public string Bio { get; set; }

        //Relationships
        public virtual ICollection<Movie> Movies { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
