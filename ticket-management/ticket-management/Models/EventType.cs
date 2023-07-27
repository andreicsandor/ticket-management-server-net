namespace ticket_management.Models;

public partial class EventType
{
    public long EventTypeId { get; set; }

    public string EventTypeName { get; set; } = string.Empty;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public override string ToString()
    {
        return EventTypeName;
    }
}
