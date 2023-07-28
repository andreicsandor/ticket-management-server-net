using ticket_management.Models;
using ticket_management.Repository;
using ticket_management.Service.Interfaces;

namespace ticket_management.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetById(long id)
        {
            var customer = await _customerRepository.GetById(id);

            return customer;
        }
    }
}