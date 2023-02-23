using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Models;
using eTickets.DTO;
using eTickets.Repositories;
using Serilog;
using Serilog.Context;
using Microsoft.AspNetCore.Authorization;

namespace eTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActorController : ControllerBase
    {
        private AppDbContext _Context;
        private readonly IActorRepository _ActorRepository;
        private readonly IUploadImageRepository _uploadImageRepository;
        //private readonly IActorContainer _container;
        private readonly ILogger<IActorRepository> _logger;

        public ActorController(AppDbContext appDbContext, IActorRepository ActorRepository, ILogger<IActorRepository> logger, IUploadImageRepository uploadImageRepository)
        {
            _Context = appDbContext;
            _ActorRepository = ActorRepository;
            _logger = logger;
            _uploadImageRepository = uploadImageRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            LogContext.PushProperty("Username", "System");

            //Log.Information("Actor Getall Troiggered");
            this._logger.LogInformation("|| Log ||Testing");
            var data = _Context.Actors.ToList();
            if(data.Count<= 0) { throw new Exception("No Data Found"); }
            return Ok(_Context.Actors);
        }

        [HttpGet("DataById")]
        public IActionResult Get(int id)
        { 
            var data = _Context.Actors.FirstOrDefault(x => x.Id == id);
            if(data.Id == id)
            {
                return Ok(data);
            }
           
            _logger.LogError("Actor Not Found");
            return BadRequest("Actor Not Found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = _Context.Actors.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                _Context.Actors.Remove(data);
                _Context.SaveChanges();
                return Ok("Deleted Succesfully......");
            }
                return BadRequest("UserNotFound");
            
        }
        [HttpPut]
        public IActionResult Put(int id, ActorDto actDto)
        {
            var data = _Context.Actors.FirstOrDefault(u => u.Id == id);
            if (data != null)
            {
                data.ProfilePictureURL = actDto.ProfilePictureURL;
                data.FullName = actDto.FullName;
                data.Bio = actDto.Bio;
                _Context.SaveChanges();
                return Ok(data);
            }
            _logger.LogError("User Not Found");
            return BadRequest("UserNotFound");  
        }


        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(ActorDto actor)
        {
            if (_Context.Actors.Any(x => x.Id == actor.Id))
            {
                return BadRequest("User already exists");
            }
            else
            {
                var ac = new Actor
                {
                    ProfilePictureURL = actor.ProfilePictureURL,
                    FullName = actor.FullName,
                    Bio = actor.Bio,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now

                };
                _Context.Actors.Add(ac);
                await _Context.SaveChangesAsync();
                return Ok("Successfully Add Data");
            }

        }

        [HttpGet("GetActorById")]
        public async Task<ActionResult> GetActor(int Id)
        {
            try
            {
                return Ok(await _ActorRepository.GetActor(Id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostAsync")]
        public async Task<IActionResult> PostAsync([FromForm] ImageAttachDto imageAttach)
        {
            if (_Context.Actors.Any(x => x.Id == imageAttach.Id))
            {
                return BadRequest("Image already added");
            }
            else
            {
                string path = await _uploadImageRepository.UploadImage(imageAttach.FileUri);
                var ins = new ImageAttach
                {
                    ActualFileUrl = path,
                    //If = imageAttach.Filename,
                    Id = imageAttach.MovieId,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now


                };
                _Context.ImageAttaches.Add(ins);

                _Context.SaveChanges();
                return Ok("Successfully Add Image");
            }
        }
    }
}

      
       
    

