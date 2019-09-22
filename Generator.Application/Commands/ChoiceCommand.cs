using System;
using MediatR;

namespace Generator.Application.DTOs
{
    /// <summary>
    /// DTO of Choice object.
    /// </summary>
    public class ChoiceCommand : IRequest
    {
        public Guid PictureAId { get; set; }

        public Guid PictureBId { get; set; }

        public Guid UserChoiceId { get; set; }
    }
}
