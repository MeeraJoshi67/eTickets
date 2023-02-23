using Microsoft.AspNetCore.Http.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class ImageAttach
    {
        [Key]
        public int Id { get; set; }
        //Movie
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [Required]
        //New Property Added
        [NotMapped]
        //[Required]
        //public string Filename { get; set; }
        public IFormFile FileUri { get; set; }
        public string ActualFileUrl { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }


        public virtual Movie Movie { get; set; }
    }
}
