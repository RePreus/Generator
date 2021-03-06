﻿using AutoMapper;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Domain.Entities;

namespace Generator.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaveChosenPicturesCommand, UserChoice>();
            CreateMap<Picture, PictureDto>();
        }
    }
}
