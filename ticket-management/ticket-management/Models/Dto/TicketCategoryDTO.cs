namespace ticket_management.Models.Dto
{
    public class TicketCategoryDTO
    {
        public long TicketCategoryId { get; set; }
        public string TicketCategoryDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
