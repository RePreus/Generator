using System;
using MediatR;

namespace Generator.Application.Commands
{
    public class SaveChosenPicturesCommand : IRequest
    {
        public Guid ChosenPictureId { get; set; }

        public Guid OtherPictureId { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }
    }
}
