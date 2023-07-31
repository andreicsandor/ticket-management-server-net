using Microsoft.EntityFrameworkCore;
using ticket_management.Exceptions;
using ticket_management.Models;

namespace ticket_management.Repository
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TicketManagementContext _dbContext;

        public TicketCategoryRepository()
        {
            _dbContext = new TicketManagementContext();
        }

        public async Task<TicketCategory> GetById(long id)
        {
            var @ticketcategory = await _dbContext.TicketCategories
                .Where(t => t.TicketCategoryId == id)
                .FirstOrDefaultAsync();

            return @ticketcategory == null ? throw new EntityNotFoundException(id, nameof(TicketCategory)) : @ticketcategory;
        }

        public async Task<TicketCategory> GetByName(string name)
        {
            var @ticketcategory = await _dbContext.TicketCategories
                .Where(t => t.TicketCategoryDescription == name)
                .FirstOrDefaultAsync();

            return @ticketcategory == null ? throw new EntityNotFoundException(name, nameof(TicketCategory)) : @ticketcategory;
        }
    }
}