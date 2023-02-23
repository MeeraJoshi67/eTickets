namespace eTickets.Repositories
{
    public class UploadImageRepository : IUploadImageRepository
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"Utility\Image", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = special + "-" + file.FileName;
            return filePath;
        }
    }
}
