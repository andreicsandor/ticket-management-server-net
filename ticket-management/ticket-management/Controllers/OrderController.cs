using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ticket_management.Models;
using ticket_management.Models.Dto;
using ticket_management.Repository;
using ticket_management.Service.Interfaces;

namespace ticket_management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IEventService _eventService;
        private readonly ICustomerService _customerService;
        private readonly ITicketCategoryService _ticketCategoryService;

        public OrderController(IOrderService orderService, IEventService eventService, ICustomerService customerService, ITicketCategoryService ticketCategoryService)
        {
            _orderService = orderService;
            _eventService = eventService;
            _customerService = customerService;
            _ticketCategoryService = ticketCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetAll()
        {
            var orders = await _orderService.GetAll();

            return Ok(orders);
        }


        [HttpGet]
        public async Task<ActionResult<OrderDTO>> GetById(long id)
        {
            var @order = await _orderService.GetById(id);

            if (@order == null)
            {
                return NotFound();
            }

            return Ok(@order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(OrderPostDTO newOrderDTO)
        {
            var @order = await _orderService.Create(newOrderDTO);

            if (@order == null)
            {
                return BadRequest();
            }

            return Ok(@order.Value);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> Patch(OrderPatchDTO orderPatch)
        {
            var result = await _orderService.Update(orderPatch);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            var result = await _orderService.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}