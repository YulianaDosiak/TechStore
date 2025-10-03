using AutoMapper;
using Model = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

public class OrderMap : Profile
{
    public OrderMap()
    {
        CreateMap<Model.Order, DTO.Order>().ReverseMap();
    }
}
