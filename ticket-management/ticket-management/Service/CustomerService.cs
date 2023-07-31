using ticket_management.Api.Exceptions;
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
            try
            {
                var customer = await _customerRepository.GetById(id);

                return customer;
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}