using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double price { get; set; }

        public int ProducerId { get; set; }

        public int MovieCategoryId { get; set; }
        //public string ImageURL { get; set; }
        
       

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is Required")]
        [JsonProperty(PropertyName = "ReleaseStartDate")]
        public DateTime ReleaseStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is Required")]
        [JsonProperty(PropertyName = "ReleaseEndDate")]
        public DateTime ReleaseEndDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // if either date is null, that date's required attribute will invalidate
            if (ReleaseStartDate != null && ReleaseEndDate != null && ReleaseStartDate >= ReleaseEndDate)
                yield return new ValidationResult("EndDate is not greater than StartDate.");
        }
    }
}
