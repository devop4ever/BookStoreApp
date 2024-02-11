using AutoMapper;
using BookStoreApp.API.Data.DTOs.Author;
using BookStoreApp.API.Data.Entities;

namespace BookStoreApp.API.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<AuthorPostDto, Author>().ReverseMap();
        CreateMap<AuthorGetDto, Author>().ReverseMap();
        CreateMap<AuthorPutDto, Author>().ReverseMap();

    }
}
