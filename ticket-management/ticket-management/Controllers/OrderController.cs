using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ticket_management.Models;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, ITicketCategoryRepository ticketCategoryRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _ticketCategoryRepository = ticketCategoryRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            /*
            var dtoOrders = orders.Select(o => new OrderDTO()
            {
                Customer = o.Customer.CustomerName,
                TicketCategory = o.TicketCategory.TicketCategoryDescription,
                OrderedAt = o.OrderedAt,
                NumberOfTickets = o.NumberOfTickets,
                TotalPrice = o.TotalPrice
            });
            */

            var dtoOrders = _mapper.Map<List<OrderDTO>>(orders);

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

            /*
            var dtoOrder = new OrderDTO()
            {
                Customer = @order.Customer.CustomerName,
                TicketCategory = @order.TicketCategory.TicketCategoryDescription,
                OrderedAt = @order.OrderedAt,
                NumberOfTickets = @order.NumberOfTickets,
                TotalPrice = @order.TotalPrice
            };
            */

            var dtoOrder = _mapper.Map<OrderDTO>(@order);

            return Ok(dtoOrder);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(OrderPostDTO newOrderDTO)
        {
            // Hardcode Customer ID
            var customer = await _customerRepository.GetById(1L);

            var ticketCategory = await _ticketCategoryRepository.GetById(newOrderDTO.TicketCategoryId);

            var numberOfTickets = newOrderDTO.NumberOfTickets;
            var date = DateTime.Now;
            var totalPrice = (decimal)numberOfTickets * ticketCategory.Price;

            var @order = new Order
            {
                CustomerId = (long)customer.CustomerId,
                TicketCategoryId = (long)ticketCategory.TicketCategoryId,
                OrderedAt = date,
                NumberOfTickets = numberOfTickets,
                TotalPrice = totalPrice
            };

            @order = await _orderRepository.Add(order);

            if (@order == null) return BadRequest("Order could not be created");

            return _mapper.Map<OrderDTO>(@order);
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