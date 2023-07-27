using Microsoft.EntityFrameworkCore;
using ticket_management.Models;

namespace ticket_management.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new TicketManagementContext();
        }

        public async Task<Order> Add(Order @order)
        {
            _dbContext.Orders.Add(@order);
            await _dbContext.SaveChangesAsync();

            var orderId = @order.OrderId;

            Order? addedOrder = await _dbContext.Orders
                .Where(e => e.OrderId == orderId)
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .FirstOrDefaultAsync();

            if (addedOrder == null)
            {
                throw new InvalidOperationException("Order not found after adding.");
            }

            return addedOrder;
        }

        public void Delete(Order @order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .ToList();

            return orders;
        }

        public async Task<Order> GetById(long id)
        {
            var @order = await _dbContext.Orders
                .Where(e => e.OrderId == id)
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .FirstOrDefaultAsync();

            return @order;
        }

        public void Update(Order @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}