using ticket_management.Models.Dto;

namespace ticket_management.Service.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAll();

        Task<IEnumerable<EventDTO>> GetAllByVenue(long venueId);

        Task<IEnumerable<EventDTO>> GetAllByType(string eventTypeName);

        Task<IEnumerable<EventDTO>> GetAllByVenueAndType(long venueId, string eventTypeName);

        Task<EventDTO> GetById(long id);

        Task<bool> Update(EventPatchDTO eventPatch);

        bool Delete(EventDTO eventDTO);
    }
}
