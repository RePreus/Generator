using System;
using MediatR;

namespace Generator.Application.Commands
{
    public class SaveChosenPicturesCommand : IRequest
    {
        public Guid ChosenPictureId { get; set; }

        public Guid OtherPictureId { get; set; }
    }
}
