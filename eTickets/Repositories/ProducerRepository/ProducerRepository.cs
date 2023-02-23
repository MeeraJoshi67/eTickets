using eTickets.Data;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Repositories.ProducerRepository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly AppDbContext _context;
        public ProducerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Producer>> GetProducer(int Id)
        {
            var result = await _context.Producers.Where(a => a.Id == Id).Include(m => m.Movies).ToListAsync();
            if (result == null)
            {
                return null;
            }
            return result;
        }
    }
}
