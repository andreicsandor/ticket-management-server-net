namespace ticket_management.Models;

public partial class Event
{
    public long EventId { get; set; }

    public long VenueId { get; set; }

    public long EventTypeId { get; set; }

    public string EventDescription { get; set; } = string.Empty;

    public string EventName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string EventImage { get; set; } = string.Empty;

    public virtual EventType? EventType { get; set; }

    public virtual ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();

    public virtual Venue? Venue { get; set; }
}
