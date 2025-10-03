using AutoMapper;
using Model = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

public class CartItemMap : Profile
{
    public CartItemMap()
    {
        CreateMap<Model.CartItem, DTO.CartItem>().ReverseMap();
    }
}
