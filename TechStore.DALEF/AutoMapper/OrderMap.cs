using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<EF.Order, DTO.Order>();
            CreateMap<DTO.Order, EF.Order>();
        }
    }
}