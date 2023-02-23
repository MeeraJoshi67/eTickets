using eTickets.Data;
using eTickets.DTO;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public AppDbContext _Context;
        public MovieController(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_Context.Movies);
        }
        [HttpGet("DataById")]
        public IActionResult Get(int id)
        {
            var data = _Context.Movies.FirstOrDefault(x => x.Id == id);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Post(MovieDto movie)
        {
            if (_Context.Movies.Any(x => x.Id == movie.Id))
            {

                return BadRequest("Movie already exists");
            }
            else
            {
                var ins = new Movie
                {

                    Name = movie.Name,
                    Description = movie.Description,
                    price = movie.price,
                    ProducerId=movie.ProducerId,
                    MovieCategoryId=movie.MovieCategoryId,
                    ReleaseStartDate= movie.ReleaseStartDate,
                    ReleaseEndDate= movie.ReleaseEndDate,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now


                };
                _Context.Movies.Add(ins);
                _Context.SaveChanges();
                return Ok("Successfully Add Data");
            }
        }
        [HttpPut]
        public IActionResult Put(int id, MovieDto movieDto)
        {
            var data = _Context.Movies.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {

                data.Name = movieDto.Name;
                data.Description = movieDto.Description;
                data.price = movieDto.price;
                data.ReleaseEndDate = movieDto.ReleaseEndDate;
                data.ReleaseStartDate = movieDto.ReleaseStartDate;
                _Context.SaveChanges();
                return Ok(data);
            }
                return BadRequest("Movie Not Found");
           
            
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = _Context.Movies.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                _Context.Movies.Remove(data);
               _Context.SaveChanges();
                return Ok("Deleted Succesfully......");
            }
                return BadRequest("Movie Not Found");
        }


    }
}

