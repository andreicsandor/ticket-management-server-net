using Microsoft.AspNetCore.Mvc;
using ticket_management.Exceptions;
using ticket_management.Models;
using ticket_management.Models.Dto;
using ticket_management.Service;
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

        [HttpGet]
        public async Task<ActionResult<EventDTO>> GetById(long id)
        {
            var @event = await _eventService.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
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
            try
            {
                var result = await _eventService.Delete(id);

                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}