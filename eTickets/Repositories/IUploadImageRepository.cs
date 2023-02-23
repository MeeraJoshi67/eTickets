namespace eTickets.Repositories
{
    public interface IUploadImageRepository
    {
        Task<string> UploadImage(IFormFile file);
    }
}
