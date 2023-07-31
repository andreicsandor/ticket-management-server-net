using Microsoft.EntityFrameworkCore;
using ticket_management.Exceptions;
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

            Order addedOrder = await _dbContext.Orders
                .Where(e => e.OrderId == orderId)
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .FirstOrDefaultAsync();

            if (addedOrder == null) throw new InvalidOperationException("Order creation failed.");

            return addedOrder;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .ToListAsync();

            return orders;
        }

        public async Task<Order> GetById(long id)
        {
            var @order = await _dbContext.Orders
                .Where(e => e.OrderId == id)
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .FirstOrDefaultAsync();

            return @order == null ? throw new EntityNotFoundException(id, nameof(Order)) : @order;
        }

        public void Update(Order @order)
        {
            try
            {
                _dbContext.Entry(@order).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the order.", ex);
            }
        }

        public void Delete(Order @order)
        {
            try
            {
                _dbContext.Remove(@order);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the order.", ex);
            }
        }
    }
}