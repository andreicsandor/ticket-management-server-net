using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ticket_management.Models;
using ticket_management.Models.Dto;
using ticket_management.Repository;
using ticket_management.Service.Interfaces;

namespace ticket_management.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerService _customerService;
        private readonly ITicketCategoryService _ticketCategoryService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, ICustomerService customerService, ITicketCategoryService ticketCategoryService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerService = customerService;
            _ticketCategoryService = ticketCategoryService;
            _mapper = mapper;
        }

        public async Task<ActionResult<OrderDTO>> Create(OrderPostDTO newOrderDTO)
        {
            // Hardcode Customer ID
            var customer = await _customerService.GetById(1L);

            var ticketCategory = await _ticketCategoryService.GetById(newOrderDTO.TicketCategoryId);

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

            if (@order == null) return null;

            return _mapper.Map<OrderDTO>(@order);
        }

        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var orders = await _orderRepository.GetAll();

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

            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetById(long id)
        {
            var @order = await _orderRepository.GetById(id);

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

            return _mapper.Map<OrderDTO>(@order);
        }

        public async Task<bool> Update(OrderPatchDTO orderPatch)
        {
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);

            if (orderEntity == null)
            {
                return false;
            }

            if (orderEntity.TicketCategory != null)
            {
                var ticketCategory = await _ticketCategoryService.GetByName(orderPatch.TicketCategory);
                orderEntity.TicketCategoryId = ticketCategory.TicketCategoryId;

                orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
                orderEntity.TotalPrice = (decimal)orderEntity.NumberOfTickets * (orderEntity.TicketCategory.Price);
            }

            _orderRepository.Update(orderEntity);

            return true;
        }

        public async Task<bool> Delete(long id)
        {
            var orderEntity = await _orderRepository.GetById(id);

            if (orderEntity == null)
            {
                return false;
            }

            _orderRepository.Delete(orderEntity);

            return true;
        }
    }
}