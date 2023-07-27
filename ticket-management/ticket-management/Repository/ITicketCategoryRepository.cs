using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface ITicketCategoryRepository
    {
        Task<TicketCategory> GetByName(string name);
        Task<TicketCategory> GetById(long id);
    }
}