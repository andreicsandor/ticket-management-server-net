using Microsoft.AspNetCore.Mvc;
using ticket_management.Api.Exceptions;
using ticket_management.Models.Dto;
using ticket_management.Service.Interfaces;

namespace ticket_management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetAll()
        {
            var events = await _eventService.GetAll();

            return Ok(events);
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDTO>>> GetEvents([FromQuery] long? venueId, [FromQuery] string? eventTypeName)
        {
            if (venueId == null && String.IsNullOrEmpty(eventTypeName))
            {
                var events = await _eventService.GetAll();
                return Ok(events);
            }
            else if (venueId != null && String.IsNullOrEmpty(eventTypeName))
            {
                var events = await _eventService.GetAllByVenue(venueId.Value);
                return Ok(events);
            }
            else if (venueId == null && !String.IsNullOrEmpty(eventTypeName))
            {
                var events = await _eventService.GetAllByType(eventTypeName);
                return Ok(events);
            }
            else if (venueId != null && !String.IsNullOrEmpty(eventTypeName))
            {
                var events = await _eventService.GetAllByVenueAndType(venueId.Value, eventTypeName);
                return Ok(events);
            }
            else
            {
                var events = new List<EventDTO>();
                return Ok(events);
            }
        }


        [HttpPatch]
        public async Task<ActionResult<EventPatchDTO>> Patch(EventPatchDTO eventPatch)
        {
            var result = await _eventService.Update(eventPatch);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            EventDTO eventDTO;

            try
            {
                eventDTO = await _eventService.GetById(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            try
            {
                var result = _eventService.Delete(eventDTO);

                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}