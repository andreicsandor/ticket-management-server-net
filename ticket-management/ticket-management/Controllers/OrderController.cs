using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ticket_management.Models.Dto;
using ticket_management.Repository;

namespace ticket_management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;

        public OrderController(IOrderRepository orderRepository, ITicketCategoryRepository ticketCategoryRepository)
        {
            _orderRepository = orderRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => new OrderDTO()
            {
                CustomerName = o.Customer.CustomerName,
                TicketCategory = o.TicketCategory.TicketCategoryDescription,
                OrderedAt = o.OrderedAt,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice
            });


            return Ok(dtoOrders);
        }


        [HttpGet]
        public async Task<ActionResult<OrderDTO>> GetById(long id)
        {
            var @order = await _orderRepository.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            var dtoOrder = new OrderDTO()
            {
                CustomerName = @order.Customer.CustomerName,
                TicketCategory = @order.TicketCategory.TicketCategoryDescription,
                OrderedAt = @order.OrderedAt,
                NumberOfTickets = @order.NumberOfTickets,
                TotalPrice = @order.TotalPrice
            };

            return Ok(dtoOrder);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> Patch(OrderPatchDTO orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);

            if (orderEntity == null)
            {
                return NotFound();
            }

            if (orderEntity.TicketCategory != null)
            {
                var ticketCategory = await _ticketCategoryRepository.GetByName(orderPatch.TicketCategory);
                orderEntity.TicketCategoryId = ticketCategory.TicketCategoryId;

                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
                orderEntity.TotalPrice = (decimal)orderEntity.NumberOfTickets * (orderEntity.TicketCategory.Price);
            }

            _orderRepository.Update(orderEntity);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var orderEntity = await _orderRepository.GetById(id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(orderEntity);

            return NoContent();
        }
    }
}