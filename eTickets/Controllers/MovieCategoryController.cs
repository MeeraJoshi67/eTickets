using eTickets.Data;
using eTickets.DTO;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCategoryController : ControllerBase
    {
        public AppDbContext _Context;
        public MovieCategoryController(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_Context.MovieCategories);
        }
        [HttpGet("DataById")]
        public IActionResult Get(int id)
        {
            var data = _Context.MovieCategories.FirstOrDefault(x => x.Id == id);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Post(MovieCategoryDto movieCategory)
        {
            if (_Context.MovieCategories.Any(x => x.Id == movieCategory.Id))
            {
                return BadRequest("Movie Category already exists");
            }
            else
            {
                var ins = new MovieCategory
                {
                   
                    Name = movieCategory.Name,
                    Description = movieCategory.Description,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now


                };
                _Context.MovieCategories.Add(ins);
                _Context.SaveChanges();
                return Ok("Successfully Add Data");
            }
        }
        [HttpPut]
        public IActionResult Put(int id,MovieCategoryDto movieCategoryDto)
        {
            var data = _Context.MovieCategories.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {

                data.Name = movieCategoryDto.Name;
                data.Description = movieCategoryDto.Description;
                _Context.SaveChanges();
                return Ok(data);

            }
                return BadRequest("Movie category Not Found");
           
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = _Context.MovieCategories.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
            _Context.MovieCategories.Remove(data);
             _Context.SaveChanges();
            return Ok("Deleted Succesfully......");
            }
                return BadRequest("Movie category Not Found");
        }


    }
}

