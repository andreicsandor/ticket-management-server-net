using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Task<Order> GetById(long id);

        int Add(Order @order);

        void Update(Order @order);

        void Delete(Order @order);
    }
}