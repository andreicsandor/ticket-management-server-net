using Microsoft.AspNetCore.Mvc;
using ticket_management.Exceptions;
using ticket_management.Models.Dto;
using ticket_management.Service.Interfaces;

namespace ticket_management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
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

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            OrderDTO orderDTO;

            try
            {
                orderDTO = await _orderService.GetById(id);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            try
            {
                var result = _orderService.Delete(orderDTO);

                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}