using AutoMapper;
using Model = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

public class CartMap : Profile
{
    public CartMap()
    {
        CreateMap<Model.Cart, DTO.Cart>().ReverseMap();
    }
}
