using AutoMapper;
using ticket_management.Models;
using ticket_management.Models.Dto;

namespace ticket_management.Profiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()
        {
            CreateMap<TicketCategory, TicketCategoryDTO>().ReverseMap();
        }
    }
}
