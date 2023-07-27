namespace ticket_management.Models;

public partial class Venue
{
    public long VenueId { get; set; }

    public string VenueLocation { get; set; } = string.Empty;

    public string VenueType { get; set; } = string.Empty;

    public int VenueCapacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public override string ToString()
    {
        return VenueLocation;
    }
}
