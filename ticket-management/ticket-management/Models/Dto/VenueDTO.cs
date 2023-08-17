namespace ticket_management.Models.Dto
{
    public class VenueDTO
    {
        public long VenueId { get; set; }

        public string VenueName { get; set; } = string.Empty;

        public string VenueLocation { get; set; } = string.Empty;

        public string VenueType { get; set; } = string.Empty;

        public int VenueCapacity { get; set; }
    }
}
