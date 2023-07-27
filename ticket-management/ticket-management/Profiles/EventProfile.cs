using AutoMapper;
using ticket_management.Models;
using ticket_management.Models.Dto;

namespace ticket_management.Profiles
{
	public class EventProfile : Profile
	{
        public EventProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, EventPatchDTO>().ReverseMap();
        }
    }
}