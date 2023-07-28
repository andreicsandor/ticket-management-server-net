using ticket_management.Models;

namespace ticket_management.Service.Interfaces
{
    public interface ITicketCategoryService
    {
        Task<TicketCategory> GetById(long id);
        Task<TicketCategory> GetByName(string name);
    }
}