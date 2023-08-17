using AutoMapper;
using ticket_management.Models;
using ticket_management.Models.Dto;

namespace ticket_management.Profiles
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueDTO>().ReverseMap();
        }
    }
}