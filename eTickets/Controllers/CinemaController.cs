using eTickets.Data;
using eTickets.DTO;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        public AppDbContext _Context;
        public CinemaController(AppDbContext appDbContext)
        {
            _Context= appDbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_Context.Cinemas);
        }
        [HttpGet("DataById")]
        public IActionResult Get(int id)
        {
            var data = _Context.Cinemas.FirstOrDefault(x => x.Id == id);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Post(CinemaDto cinema)
        {
            if (_Context.Cinemas.Any(x => x.Id == cinema.Id))
            {
                return BadRequest("Cinema already exists");
            }
            else
            {
                var ins = new Cinema
                {
                    Logo = cinema.Logo,
                    Name = cinema.Name,
                    Description = cinema.Description,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now


                };
                _Context.Cinemas.Add(ins);
                _Context.SaveChanges();
                return Ok("Successfully Add Data");
            }
        }
        [HttpPut]
        public IActionResult Put(int id,CinemaDto cinemaDto)
        {
            var data = _Context.Cinemas.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                data.Logo = cinemaDto.Logo;
                data.Name = cinemaDto.Name;
                data.Description = cinemaDto.Description;
                _Context.SaveChanges();
                return Ok(data);

            }
                return BadRequest("Cinema Not Found");
         
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = _Context.Cinemas.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                _Context.Cinemas.Remove(data);
                _Context.SaveChanges();
                return Ok("Deleted Succesfully......");
            }
                return BadRequest("Cinema Not Found");
        }


    }
}
