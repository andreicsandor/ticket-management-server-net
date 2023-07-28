using ticket_management.Models.Dto;

namespace ticket_management.Service.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAll();
        Task<EventDTO> GetById(long id);
        Task<bool> Update(EventPatchDTO eventPatch);
        Task<bool> Delete(long id);
    }
}
