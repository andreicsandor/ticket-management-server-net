using System.Collections.Generic;
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

            Order addedOrder = await _dbContext.Orders
                .Where(e => e.OrderId == orderId)
                .Include(o => o.Customer)
                .Include(o => o.TicketCategory)
                .FirstOrDefaultAsync();

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

            return @order;
        }

        public void Update(Order @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


        public void Delete(Order @order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }
    }
}