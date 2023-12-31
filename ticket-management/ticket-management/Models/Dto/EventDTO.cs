﻿namespace ticket_management.Models.Dto
{
    public class EventDTO
    {
        public long EventId { get; set; }

        public string EventDescription { get; set; } = string.Empty;

        public string EventName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string EventImage { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public VenueDTO Venue { get; set; }

        public List<TicketCategoryDTO> TicketCategories { get; set; } = new List<TicketCategoryDTO>();
    }
}
