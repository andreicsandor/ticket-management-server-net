using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Task<Order> GetById(long id);

        Task<Order> Add(Order @order);

        void Update(Order @order);

        void Delete(Order @order);
    }
}