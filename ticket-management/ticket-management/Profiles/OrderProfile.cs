using AutoMapper;
using ticket_management.Models;
using ticket_management.Models.Dto;

namespace ticket_management.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderPatchDTO>().ReverseMap();
        }
    }
}