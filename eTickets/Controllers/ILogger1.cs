namespace eTickets.Controllers
{
    internal interface ILogger
    {
        object ForContext(string v, Guid guid);
        object ForContext(string v, object guid);
    }
}