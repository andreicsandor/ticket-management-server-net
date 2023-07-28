using AutoMapper;
using ticket_management.Models.Dto;
using ticket_management.Repository;
using ticket_management.Service.Interfaces;

namespace ticket_management.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDTO>> GetAll()
        {
            var events = await _eventRepository.GetAll();

            /*
            var dtoEvents = events.Select(e => new EventDTO()
            {
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType.EventTypeName,
                Venue = e.Venue?.VenueLocation
            });
            */

            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<EventDTO> GetById(long id)
        {
            var @event = await _eventRepository.GetById(id);

            /*
            var dtoEvent = new EventDTO()
            {
                EventDescription = @event.EventDescription,
                EventName = @event.EventName,
                EventType = @event.EventType.EventTypeName,
                Venue = @event.Venue?.VenueLocation
            };
            */

            return _mapper.Map<EventDTO>(@event);
        }

        public async Task<bool> Update(EventPatchDTO eventPatch)
        {
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

            if (eventEntity == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(eventPatch.EventName)) eventEntity.EventName = eventPatch.EventName;
            if (!string.IsNullOrEmpty(eventPatch.EventDescription)) eventEntity.EventDescription = eventPatch.EventDescription;

            _eventRepository.Update(eventEntity);

            return true;

        }

        public async Task<bool> Delete(long id)
        {
            var eventEntity = await _eventRepository.GetById(id);

            if (eventEntity == null)
            {
                return false;
            }

            _eventRepository.Delete(eventEntity);

            return true;
        }
    }
}