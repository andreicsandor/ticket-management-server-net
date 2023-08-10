using Microsoft.EntityFrameworkCore;
using ticket_management.Exceptions;
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

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = await _dbContext.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .Include(e => e.TicketCategories)
                .ToListAsync();

            return events;
        }

        public async Task<IEnumerable<Event>> GetAllByVenue(long venueId)
        {
            var events = await _dbContext.Events
                .Where(e => e.VenueId == venueId)
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .Include(e => e.TicketCategories)
                .ToListAsync();

            return events;
        }

        public async Task<IEnumerable<Event>> GetAllByType(string eventTypeName)
        {
            var events = await _dbContext.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .Include(e => e.TicketCategories)
                .Where(e => e.EventType.EventTypeName == eventTypeName)
                .ToListAsync();

            return events;
        }

        public async Task<IEnumerable<Event>> GetAllByVenueAndType(long venueId, string eventTypeName)
        {
            var events = await _dbContext.Events
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .Include(e => e.TicketCategories)
                .Where(e => e.VenueId == venueId && e.EventType.EventTypeName == eventTypeName)
                .ToListAsync();

            return events;
        }

        public async Task<Event> GetById(long id)
        {
            var @event = await _dbContext.Events
                .Where(e => e.EventId == id)
                .Include(e => e.EventType)
                .Include(e => e.Venue)
                .Include(e => e.TicketCategories)
                .FirstOrDefaultAsync();

            return @event == null ? throw new EntityNotFoundException(id, nameof(Event)) : @event;
        }

        public void Update(Event @event)
        {
            try
            {
                _dbContext.Entry(@event).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the event.", ex);
            }
        }

        public void Delete(Event @event)
        {
            try
            {
                _dbContext.Remove(@event);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the event.", ex);
            }
        }
    }
}