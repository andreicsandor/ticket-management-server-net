using ticket_management.Exceptions;
using ticket_management.Models;
using ticket_management.Repository;
using ticket_management.Service.Interfaces;

namespace ticket_management.Service
{
    public class TicketCategoryService : ITicketCategoryService
    {
        private readonly ITicketCategoryRepository _ticketCategoryRepository;

        public TicketCategoryService(ITicketCategoryRepository ticketCategoryRepository)
        {
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        public async Task<TicketCategory> GetById(long id)
        {
            try
            {
                var ticketCategory = await _ticketCategoryRepository.GetById(id);

                return ticketCategory;
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }

        public async Task<TicketCategory> GetByName(string name)
        { 
            try
            {
                var ticketCategory = await _ticketCategoryRepository.GetByName(name);

                return ticketCategory;
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}