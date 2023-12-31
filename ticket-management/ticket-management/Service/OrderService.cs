﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ticket_management.Exceptions;
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
            try
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
                    EventId = (long)ticketCategory.EventId,
                    TicketCategoryId = (long)ticketCategory.TicketCategoryId,
                    OrderedAt = date,
                    NumberOfTickets = numberOfTickets,
                    TotalPrice = totalPrice
                };

                @order = await _orderRepository.Add(order);

                return _mapper.Map<OrderDTO>(@order);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
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
            try
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
            catch (EntityNotFoundException)
            {
                return null;
            }
        }

        public async Task<bool> Update(OrderPatchDTO orderPatch)
        {
            try
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

                    var price = ticketCategory.Price;
                    orderEntity.TotalPrice = (decimal)orderEntity.NumberOfTickets * price;
                }

                _orderRepository.Update(orderEntity);

                return true;
            }
            catch (EntityNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(long orderId)
        {
            try
            {
                var orderEntity = await _orderRepository.GetById(orderId);

                if (orderEntity == null)
                {
                    throw new EntityNotFoundException();
                }

                _orderRepository.Delete(orderEntity);

                return true;
            }
            catch (EntityNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}