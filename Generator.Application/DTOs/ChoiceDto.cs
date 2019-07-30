using System;
using Generator.Application.Models;
using MediatR;

namespace Generator.Application.DTOs
{
    /// <summary>
    /// DTO of Choice object.
    /// </summary>
    public class ChoiceDto : IRequest<RequestResult>
    {
        public Guid PictureA { get; set; }

        public Guid PictureB { get; set; }

        public Guid UserChoice { get; set; }
    }
}
