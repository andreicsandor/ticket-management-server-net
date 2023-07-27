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

        public int Add(Order @order)
        {
            throw new NotImplementedException();
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