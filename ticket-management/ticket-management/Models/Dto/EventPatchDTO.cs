namespace ticket_management.Models.Dto
{
    public class EventPatchDTO
    {
        public long EventId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;
    }
}