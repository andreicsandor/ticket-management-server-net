namespace ticket_management.Models.Dto
{
    public class OrderPatchDTO
    {
        public long OrderId { get; set; }

        public string TicketCategory { get; set; } = string.Empty;

        public int NumberOfTickets { get; set; }
    }
}