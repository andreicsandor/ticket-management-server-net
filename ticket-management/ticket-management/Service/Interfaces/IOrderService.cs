using Microsoft.AspNetCore.Mvc;
using ticket_management.Models.Dto;

namespace ticket_management.Service.Interfaces
{
    public interface IOrderService
    {
        Task<ActionResult<OrderDTO>> Create(OrderPostDTO newOrderDTO);

        Task<IEnumerable<OrderDTO>> GetAll();

        Task<OrderDTO> GetById(long id);

        Task<bool> Update(OrderPatchDTO orderPatch);

        bool Delete(OrderDTO orderDTO);
    }
}