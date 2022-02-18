using AutoMapper;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Application, ApplicationDto>().ReverseMap();
        }
        
    }
}
