using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ticket_management.Models.Dto;
using ticket_management.Repository;

namespace ticket_management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetAll()
        {
            var events = _eventRepository.GetAll();

            /*
            var dtoEvents = events.Select(e => new EventDTO()
            {
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType.EventTypeName,
                Venue = e.Venue?.VenueLocation
            });
            */

            var dtoEvents = _mapper.Map<List<EventDTO>>(events);

            return Ok(dtoEvents);
        }


        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetById(long id)
        {
            var @event = await _eventRepository.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            /*
            var dtoEvent = new EventDTO()
            {
                EventDescription = @event.EventDescription,
                EventName = @event.EventName,
                EventType = @event.EventType.EventTypeName,
                Venue = @event.Venue?.VenueLocation
            };
            */

            var dtoEvent = _mapper.Map<EventDTO>(@event);

            return Ok(dtoEvent);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDTO>> Patch(EventPatchDTO eventPatch)
        {
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

            if (eventEntity == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(eventPatch.EventName)) eventEntity.EventName = eventPatch.EventName;
            if (!string.IsNullOrEmpty(eventPatch.EventDescription)) eventEntity.EventDescription = eventPatch.EventDescription;

            _eventRepository.Update(eventEntity);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var eventEntity = await _eventRepository.GetById(id);

            if (eventEntity == null)
            {
                return NotFound();
            }

            _eventRepository.Delete(eventEntity);

            return NoContent();
        }
    }
}