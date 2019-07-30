using AutoMapper;
using Generator.Application.DTOs;
using Generator.Domain;

namespace Generator.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChoiceDto, Choice>();
        }
    }
}
