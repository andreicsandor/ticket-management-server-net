namespace ticket_management.Models;

public partial class TicketCategory
{
    public long TicketCategoryId { get; set; }

    public long EventId { get; set; }

    public string TicketCategoryDescription { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return TicketCategoryDescription;
    }
}