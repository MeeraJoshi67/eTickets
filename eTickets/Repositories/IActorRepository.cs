using eTickets.Models;

namespace eTickets.Repositories
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetActor(int Id);
    }
}
