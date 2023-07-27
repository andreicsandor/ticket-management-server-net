namespace ticket_management.Models.Dto
{
	public class OrderDTO
	{
        public string CustomerName { get; set; } = string.Empty;

        public string TicketCategory { get; set; } = string.Empty;

        public DateTime OrderedAt { get; set; }

        public int NumberOfTickets { get; set; }

        public decimal TotalPrice { get; set; }
    }
}