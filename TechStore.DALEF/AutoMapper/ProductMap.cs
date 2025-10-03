using AutoMapper;
using Model = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

public class ProductMap : Profile
{
    public ProductMap()
    {
        CreateMap<Model.Product, DTO.Product>().ReverseMap();
    }
}
