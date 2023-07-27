namespace ticket_management.Models;

public partial class Customer
{
    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
