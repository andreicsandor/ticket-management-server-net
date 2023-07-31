using Microsoft.AspNetCore.Mvc;
using Moq;
using ticket_management.Controllers;
using ticket_management.Exceptions;
using ticket_management.Models.Dto;
using ticket_management.Service.Interfaces;

namespace ticket_management_test
{
    [TestClass]
    public class OrderControllerTest
    {
        private Mock<IOrderService> _orderServiceMock;
        private List<OrderDTO> _orderList;
        private OrderPatchDTO _orderPatchDTO;
        private OrderPatchDTO _nonExistingOrderPatchDTO;
        private OrderPostDTO _newOrderPostDTO;
        private OrderDTO _newOrderDTO;

        [TestInitialize]
        public void Setup()
        {
            _orderServiceMock = new Mock<IOrderService>();

            _orderList = new List<OrderDTO>
            {
                new OrderDTO
                {
                    Customer = "Mock Customer 1",
                    TicketCategory = "Mock Ticket Category 1",
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 2,
                    TotalPrice = 100.00m
                },
                new OrderDTO
                {
                    Customer = "Mock Customer 2",
                    TicketCategory = "Mock Ticket Category 2",
                    OrderedAt = DateTime.Now.AddDays(-1),
                    NumberOfTickets = 1,
                    TotalPrice = 50.00m
                }
            };

            _orderPatchDTO = new OrderPatchDTO
            {
                OrderId = 1,
                TicketCategory = "Mock Ticket Category 1",
                NumberOfTickets = 3
            };

            _nonExistingOrderPatchDTO = new OrderPatchDTO
            {
                OrderId = 99,
                TicketCategory = "Mock Ticket Category 99",
                NumberOfTickets = 2
            };

            _newOrderPostDTO = new OrderPostDTO
            {
                TicketCategoryId = 1,
                NumberOfTickets = 2
            };

            _newOrderDTO = new OrderDTO
            {
                Customer = "Mock Customer 3",
                TicketCategory = "Mock Ticket Category 3",
                OrderedAt = DateTime.Now,
                NumberOfTickets = 2,
                TotalPrice = 100.00m
            };
        }

        [TestMethod]
        public async Task GetAllReturnList()
        {
            // Arrange
            _orderServiceMock.Setup(moq => moq.GetAll()).Returns(Task.FromResult(_orderList as IEnumerable<OrderDTO>));
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var orders = await controller.GetAll();
            var orderResult = orders.Result as OkObjectResult;
            var orderDTOList = orderResult.Value as IEnumerable<OrderDTO>;

            // Assert
            Assert.AreEqual(_orderList.Count, orderDTOList.Count());
        }

        [TestMethod]
        public async Task GetByIdReturnOk()
        {
            // Arrange
            var orderId = 1;
            _orderServiceMock.Setup(moq => moq.GetById(orderId)).Returns(Task.FromResult(_orderList.First()));
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var order = await controller.GetById(orderId);
            var orderResult = order.Result as OkObjectResult;
            var orderDTO = orderResult.Value as OrderDTO;

            // Assert
            Assert.AreEqual(_orderList.First().Customer, orderDTO.Customer);
        }

        [TestMethod]
        public async Task GetByIdReturnNotFound()
        {
            // Arrange
            var orderId = 99;
            _orderServiceMock.Setup(moq => moq.GetById(orderId)).ReturnsAsync((OrderDTO)null);
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var result = await controller.GetById(orderId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PatchReturnOk()
        {
            // Arrange
            _orderServiceMock.Setup(moq => moq.Update(_orderPatchDTO)).Returns(Task.FromResult(true));
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var result = await controller.Patch(_orderPatchDTO);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PatchReturnNotFound()
        {
            // Arrange
            _orderServiceMock.Setup(moq => moq.Update(_nonExistingOrderPatchDTO)).Returns(Task.FromResult(false));
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var result = await controller.Patch(_nonExistingOrderPatchDTO);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteReturnOk()
        {
            // Arrange
            var orderId = 1;
            var orderDTO = _orderList.First();
            _orderServiceMock.Setup(moq => moq.GetById(orderId)).ReturnsAsync(orderDTO);
            _orderServiceMock.Setup(moq => moq.Delete(orderDTO)).Returns(true);
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var result = await controller.Delete(orderId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteReturnNotFound()
        {
            // Arrange
            var orderId = 99;
            _orderServiceMock.Setup(moq => moq.GetById(orderId)).ThrowsAsync(new EntityNotFoundException());
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var result = await controller.Delete(orderId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateReturnOk()
        {
            // Arrange
            _orderServiceMock.Setup(moq => moq.Create(_newOrderPostDTO)).Returns(Task.FromResult<ActionResult<OrderDTO>>(_newOrderDTO));
            var controller = new OrderController(_orderServiceMock.Object);

            // Act
            var result = await controller.Create(_newOrderPostDTO);
            var orderResult = result.Result as OkObjectResult;
            var orderDTO = orderResult.Value as OrderDTO;

            // Assert
            Assert.AreEqual(_newOrderDTO.Customer, orderDTO.Customer);
        }
    }
}
