using AutoMapper;
using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Domain.Entities;

namespace FilmsTestTask.APIServices.Mapping.Profiles
{
    public class FilmProfile : Profile
    {
        public FilmProfile() 
        {
            CreateMap<Film, FilmApiModel>().ReverseMap();

            CreateMap<FilmCategory, FilmCategoryApiModel>().ReverseMap();
        }
    }
}
