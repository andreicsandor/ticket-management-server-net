using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAll();

        Task<IEnumerable<Event>> GetAllByVenue(long venueId);

        Task<IEnumerable<Event>> GetAllByType(string eventTypeName);

        Task<IEnumerable<Event>> GetAllByVenueAndType(long venueId, string eventTypeName);

        Task<Event> GetById(long id);

        void Update(Event @event);

        void Delete(Event @event);
    }
}