using AutoMapper;
using Generator.Application.DTOs;
using Generator.Domain.Entities;

namespace Generator.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChoiceCommand, Choice>();
        }
    }
}
