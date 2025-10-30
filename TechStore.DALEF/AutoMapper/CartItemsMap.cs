using AutoMapper;
using EF = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

namespace TechStore.DALEF.AutoMapper
{
    public class CartItemsMap : Profile
    {
        public CartItemsMap()
        {
            CreateMap<EF.CartItem, DTO.CartItems>();
            CreateMap<DTO.CartItems, EF.CartItem>();
        }
    }
}