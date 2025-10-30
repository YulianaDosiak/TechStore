using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class OrderItemMap : Profile
    {
        public OrderItemMap()
        {
            CreateMap<EF.OrderItem, DTO.OrderItem>();
            CreateMap<DTO.OrderItem, EF.OrderItem>();
        }
    }
}