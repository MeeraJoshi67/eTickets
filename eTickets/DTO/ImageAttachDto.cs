namespace eTickets.DTO
{
    public class ImageAttachDto
    {
        public int Id { get; set; }
        public IFormFile FileUri { get; set; }

        public int MovieId { get; set; }
    }
}
