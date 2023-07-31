using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(long id);
    }
}