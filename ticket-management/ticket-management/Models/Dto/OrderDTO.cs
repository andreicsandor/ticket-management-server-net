namespace ticket_management.Models.Dto
{
	public class OrderDTO
	{
        public long OrderId { get; set; }

        public string Customer { get; set; } = string.Empty;

        public long EventId { get; set; }

        public string TicketCategory { get; set; } = string.Empty;

        public DateTime OrderedAt { get; set; }

        public int NumberOfTickets { get; set; }

        public decimal TotalPrice { get; set; }
    }
}