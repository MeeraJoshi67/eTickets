using eTickets.Models;

namespace eTickets.Repositories.ProducerRepository
{
    public interface IProducerRepository
    {
        Task<List<Producer>> GetProducer(int Id);
    }
}
