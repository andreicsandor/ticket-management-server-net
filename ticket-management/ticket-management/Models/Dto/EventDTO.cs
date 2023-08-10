namespace ticket_management.Models.Dto
{
    public class EventDTO
    {
        public string EventDescription { get; set; } = string.Empty;

        public string EventName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string EventImage { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public string Venue { get; set; } = string.Empty;

        public List<TicketCategoryDTO> TicketCategories { get; set; } = new List<TicketCategoryDTO>();
    }
}
