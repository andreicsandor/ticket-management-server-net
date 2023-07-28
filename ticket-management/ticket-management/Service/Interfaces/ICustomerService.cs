using ticket_management.Models;

namespace ticket_management.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetById(long id);
    }
}
