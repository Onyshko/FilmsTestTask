using AutoMapper;
using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Domain.Entities;

namespace FilmsTestTask.APIServices.Mapping.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryApiModel>().ReverseMap();
        }
    }
}
