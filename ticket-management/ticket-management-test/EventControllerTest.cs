using Microsoft.AspNetCore.Mvc;
using Moq;
using ticket_management.Controllers;
using ticket_management.Exceptions;
using ticket_management.Models;
using ticket_management.Models.Dto;
using ticket_management.Service.Interfaces;

namespace ticket_management_test
{
    [TestClass]
    public class EventsControllerTest
    {
        Mock<IEventService> _eventServiceMock;
        List<EventDTO> _dtoMoq;
        EventPatchDTO _eventPatchDTO;
        EventPatchDTO _nonExistingEventPatchDTO;

        [TestInitialize]
        public void SetupMoqData()
        {
            _eventServiceMock = new Mock<IEventService>();

            _dtoMoq = new List<EventDTO>
            {
                new EventDTO
                {
                    EventDescription = "Mock Description",
                    EventName = "Mock Name",
                    EventType = new EventType {EventTypeId = 1,EventTypeName="Test Event Type"}.EventTypeName,
                    Venue = new Venue {VenueId = 1, VenueCapacity = 12, VenueLocation = "Mock Location", VenueType = "Mock Type"}.VenueLocation
                }
            };

            _eventPatchDTO = new EventPatchDTO
            {
                EventId = 1,
                EventName = "Updated Event",
                EventDescription = "Updated Description"
            };

            _nonExistingEventPatchDTO = new EventPatchDTO
            {
                EventId = 99,
                EventName = "Non-Existing Event",
                EventDescription = "Non-Existing Description"
            };
        }

        [TestMethod]
        public async Task GetEventsReturnList()
        {
            // Arrange
            _eventServiceMock.Setup(moq => moq.GetAll()).Returns(Task.FromResult(_dtoMoq as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetAll();
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(_dtoMoq.Count, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventsReturnListByVenue()
        {
            // Arrange
            var venueId = 1;
            _eventServiceMock.Setup(moq => moq.GetAllByVenue(venueId)).Returns(Task.FromResult(_dtoMoq as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetEvents(venueId, null);
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(_dtoMoq.Count, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventsReturnEmptyListByVenue()
        {
            // Arrange
            var venueId = 1;
            var emptyList = new List<EventDTO>();
            _eventServiceMock.Setup(moq => moq.GetAllByVenue(venueId)).Returns(Task.FromResult(emptyList as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetEvents(venueId, null);
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(0, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventsReturnListByType()
        {
            // Arrange
            var eventTypeName = "test event type";
            _eventServiceMock.Setup(moq => moq.GetAllByType(eventTypeName)).Returns(Task.FromResult(_dtoMoq as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetEvents(null, eventTypeName);
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(_dtoMoq.Count, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventsReturnEmptyListByType()
        {
            // Arrange
            var eventTypeName = "Non-Existent Event Type";
            var emptyList = new List<EventDTO>();
            _eventServiceMock.Setup(moq => moq.GetAllByType(eventTypeName)).Returns(Task.FromResult(emptyList as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetEvents(null, eventTypeName);
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(0, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventsReturnListByVenueAndType()
        {
            // Arrange
            var venueId = 1;
            var eventTypeName = "Test Event Type";
            _eventServiceMock.Setup(moq => moq.GetAllByVenueAndType(venueId, eventTypeName)).Returns(Task.FromResult(_dtoMoq as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetEvents(venueId, eventTypeName);
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(_dtoMoq.Count, eventDTOList.Count());
        }

        [TestMethod]
        public async Task GetEventsReturnEmptyListByVenueAndType()
        {
            // Arrange
            var venueId = 1;
            var eventTypeName = "Non-Existent Event Type";
            var emptyList = new List<EventDTO>();
            _eventServiceMock.Setup(moq => moq.GetAllByVenueAndType(venueId, eventTypeName)).Returns(Task.FromResult(emptyList as IEnumerable<EventDTO>));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var events = await controller.GetEvents(venueId, eventTypeName);
            var eventResult = events.Result as OkObjectResult;
            var eventDTOList = eventResult.Value as IEnumerable<EventDTO>;

            // Assert
            Assert.AreEqual(0, eventDTOList.Count());
        }

        [TestMethod]
        public async Task PatchReturnOk()
        {
            // Arrange
            _eventServiceMock.Setup(moq => moq.Update(_eventPatchDTO)).Returns(Task.FromResult(true));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var result = await controller.Patch(_eventPatchDTO);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PatchReturnNotFound()
        {
            // Arrange
            _eventServiceMock.Setup(moq => moq.Update(_nonExistingEventPatchDTO)).Returns(Task.FromResult(false));
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var result = await controller.Patch(_nonExistingEventPatchDTO);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteReturnOk()
        {
            // Arrange
            var eventId = 1;
            var eventDTO = new EventDTO
            {
                EventName = "Existing Event",
                EventDescription = "Existing Description"
            };
            _eventServiceMock.Setup(moq => moq.GetById(eventId)).Returns(Task.FromResult(eventDTO));
            _eventServiceMock.Setup(moq => moq.Delete(eventDTO)).Returns(true);
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var result = await controller.Delete(eventId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteReturnNotFound()
        {
            // Arrange
            var eventId = 99;
            _eventServiceMock.Setup(moq => moq.GetById(eventId)).ThrowsAsync(new EntityNotFoundException());
            var controller = new EventController(_eventServiceMock.Object);

            // Act
            var result = await controller.Delete(eventId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
