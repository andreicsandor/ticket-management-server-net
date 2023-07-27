using Microsoft.EntityFrameworkCore;
using ticket_management.Models;

namespace ticket_management.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementContext();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .ToList();

            return events;
        }

        public async Task<Event> GetById(long id)
        {
            var @event = await _dbContext.Events
                .Where(e => e.EventId == id)
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .FirstOrDefaultAsync();

            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}