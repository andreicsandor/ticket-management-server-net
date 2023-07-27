using Microsoft.EntityFrameworkCore;
using ticket_management.Models;

namespace ticket_management.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TicketManagementContext _dbContext;

        public CustomerRepository()
        {
            _dbContext = new TicketManagementContext();
        }

        public async Task<Customer> GetById(long id)
        {
            var customer = await _dbContext.Customers
                .Where(e => e.CustomerId == id)
                .FirstOrDefaultAsync();

            return customer;
        }
    }
}