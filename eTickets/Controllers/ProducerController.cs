using eTickets.Data;
using eTickets.DTO;
using eTickets.Models;
using eTickets.Repositories.ProducerRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;

namespace eTickets.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly IProducerRepository _producerRepository;

        public ProducerController(AppDbContext appDbContext, IProducerRepository producerRepository)
        {
            _Context = appDbContext;
            _producerRepository= producerRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            LogContext.PushProperty("Username", "Producer");
            return Ok(_Context.Producers.ToListAsync());
        }
        [HttpGet("DataById")]
        public IActionResult Get(int id)
        {
            LogContext.PushProperty("Username", "Producer");
            var data = _Context.Producers.FirstOrDefault(x => x.Id == id);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Post(ProducerDto producer)
        {
            if (_Context.Cinemas.Any(x => x.Id == producer.Id))
            {
                return BadRequest("Producer already exists");
            }
            else
            {
                var ins = new Producer
                {
                    ProfilePictureURL = producer.ProfilePictureURL,
                    FullName = producer.FullName,
                    Bio = producer.Bio,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now


                };
                _Context.Producers.Add(ins);
                _Context.SaveChanges();
                return Ok("Successfully Add Data");
            }
        }
        [HttpPut]
        public IActionResult Put(int id,ProducerDto producerDto)
        {
            var data = _Context.Producers.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {

                data.ProfilePictureURL = producerDto.ProfilePictureURL;
                data.FullName = producerDto.FullName;
                data.Bio = producerDto.Bio;
                _Context.SaveChanges();
                return Ok(data);
            }
                return BadRequest("producer Not Found");
            
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = _Context.Producers.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                _Context.Producers.Remove(data);
                _Context.SaveChanges();
                return Ok("Deleted Succesfully......");
            }
                return BadRequest("producer Not Found");
        }

        [HttpGet("GetProducerById")]
        public async Task<ActionResult> GetProducer(int Id)
        {
            try
            {
                return Ok(await _producerRepository.GetProducer(Id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}

