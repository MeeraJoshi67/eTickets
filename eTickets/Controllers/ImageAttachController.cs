using eTickets.Data;
using eTickets.DTO;
using eTickets.Models;
using eTickets.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageAttachController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly IUploadImageRepository _UploadImageRepository;
        public ImageAttachController(AppDbContext appDbContext, IUploadImageRepository UploadImageRepository)
        {
            _Context = appDbContext;
            _UploadImageRepository = UploadImageRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_Context.Cinemas);
        }
        [HttpGet("DataById")]
        public IActionResult Get(int id)
        {
            var data = _Context.ImageAttaches.FirstOrDefault(x => x.Id == id);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] ImageAttachDto imageAttach)
        {
            if (_Context.Movies.Any(x => x.Id == imageAttach.Id))
            {
                return BadRequest("Image already added");
            }
            else
            {
                string path = await _UploadImageRepository.UploadImage(imageAttach.FileUri);
                var ins = new ImageAttach
                {
                    ActualFileUrl = path,
                    //If = imageAttach.Filename,
                    MovieId = imageAttach.MovieId,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now


                };
                _Context.ImageAttaches.Add(ins);

                _Context.SaveChanges();
                return Ok("Successfully Add Image");
            }
        }


        //[HttpPut]
        //public IActionResult Put(int id, ImageAttachDto imageAttachDto)
        //{
        //    var data = _Context.ImageAttaches.FirstOrDefault(u => u.Id == id);
        //    if (data != null)
        //    {
        //        data.ActualFileUrl = imageAttachDto.ActualFileUrl;
        //        data.FileUri = imageAttachDto.FileUri;
        //        _Context.SaveChanges();
        //        return Ok(data);
        //    }
        //    return BadRequest("ImageNotFound");

        //}
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = _Context.ImageAttaches.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                _Context.ImageAttaches.Remove(data);
                _Context.SaveChanges();
                return Ok("Deleted Succesfully......");
            }
            return BadRequest("ImageNotFound");
        }


    }
}

