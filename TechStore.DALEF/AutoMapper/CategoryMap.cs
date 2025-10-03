using AutoMapper;
using Model = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

public class CategoryMap : Profile
{
    public CategoryMap()
    {
        CreateMap<Model.Category, DTO.Category>().ReverseMap();
    }
}
