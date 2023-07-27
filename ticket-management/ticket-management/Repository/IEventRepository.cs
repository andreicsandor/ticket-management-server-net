using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event> GetById(long id);

        void Update(Event @event);

        void Delete(Event @event);
    }
}