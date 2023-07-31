using ticket_management.Models;

namespace ticket_management.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order @order);

        Task<IEnumerable<Order>> GetAll();

        Task<Order> GetById(long id);

        void Update(Order @order);

        void Delete(Order @order);
    }
}