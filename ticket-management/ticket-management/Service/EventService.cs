using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ticket_management.Api.Exceptions;
using ticket_management.Models;
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

        public async Task<IEnumerable<EventDTO>> GetAllByVenue(long venueId)
        {
            var events = await _eventRepository.GetAllByVenue(venueId);

            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<IEnumerable<EventDTO>> GetAllByType(string eventTypeName)
        {
            var events = await _eventRepository.GetAllByType(eventTypeName);

            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<IEnumerable<EventDTO>> GetAllByVenueAndType(long venueId, string eventTypeName)
        {
            var events = await _eventRepository.GetAllByVenueAndType(venueId, eventTypeName);

            return _mapper.Map<List<EventDTO>>(events);
        }

        public async Task<EventDTO> GetById(long id)
        {
            try
            {
                var @event = await _eventRepository.GetById(id);

                return _mapper.Map<EventDTO>(@event);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }

        public async Task<bool> Update(EventPatchDTO eventPatch)
        {
            try
            {
                var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

                if (!string.IsNullOrEmpty(eventPatch.EventName)) eventEntity.EventName = eventPatch.EventName;
                if (!string.IsNullOrEmpty(eventPatch.EventDescription)) eventEntity.EventDescription = eventPatch.EventDescription;

                _eventRepository.Update(eventEntity);

                return true;
            }
            catch (EntityNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(EventDTO eventDTO)
        {
            try
            {
                var eventEntity = _mapper.Map<Event>(eventDTO);

                _eventRepository.Delete(eventEntity);

                return true;
            }
            catch (EntityNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}