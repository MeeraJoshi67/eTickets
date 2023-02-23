using eTickets.Data;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ActorRepository> _logger;

        public ActorRepository(AppDbContext context, ILogger<ActorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Actor>> GetActor(int Id)
        {
           
            var result = await _context.Actors.Where(a => a.Id == Id).Include(m => m.Actors_Movies).ToListAsync();
            if(result == null)
            {
                _logger.LogError("Actor Not Found");
                throw new Exception("Act");
            }
            return result;
        }
    }
}
