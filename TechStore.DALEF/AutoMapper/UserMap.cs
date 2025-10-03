using AutoMapper;
using Model = TechStore.DALEF.Models;
using DTO = TechStore.DTO;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<Model.User, DTO.User>().ReverseMap();
    }
}
