using System;
using Generator.Application.Models;
using MediatR;

namespace Generator.Application.DTOs
{
    /// <summary>
    /// DTO of Payload object.
    /// </summary>
    public class PayloadDto : IRequest<RequestResult>
    {
        public Guid PictureA { get; set; }

        public Guid PictureB { get; set; }

        public Guid UserChoice { get; set; }

        public PayloadDto(string pictureA, string pictureB, string userchoice)
        {
            this.PictureA = new Guid(pictureA);
            this.PictureB = new Guid(pictureB);
            this.UserChoice = new Guid(userchoice);
        }
    }
}
