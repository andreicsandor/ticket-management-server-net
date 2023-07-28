using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface ITicketCategoryRepository
    {
        Task<TicketCategory> GetById(long id);
        Task<TicketCategory> GetByName(string name);
    }
}