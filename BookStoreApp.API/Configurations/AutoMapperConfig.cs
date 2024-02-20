using AutoMapper;
using BookStoreApp.API.Data.DTOs.Author;
using BookStoreApp.API.Data.DTOs.Book;
using BookStoreApp.API.Data.DTOs.User;
using BookStoreApp.API.Data.Entities;

namespace BookStoreApp.API.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<AuthorPostDto, Author>().ReverseMap();
        CreateMap<AuthorGetDto, Author>().ReverseMap();
        CreateMap<AuthorPutDto, Author>().ReverseMap();

        CreateMap<Book, BookGetDto>()
            .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author!.FirstName} {map.Author.LastName}" ))
            .ReverseMap();

        CreateMap<Book, BookPostDto>().ReverseMap();
        CreateMap<Book, BookPutDto>().ReverseMap();

        CreateMap<ApiUser, UserDto>().ReverseMap();
    }
}
