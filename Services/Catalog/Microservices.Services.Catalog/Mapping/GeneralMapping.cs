using AutoMapper;
using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;

namespace Microservices.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
           
            CreateMap<CourseCreateDto, Course>().ReverseMap();
            CreateMap<CourseUpdateDto, Course>().ReverseMap();
        }
    }
}
