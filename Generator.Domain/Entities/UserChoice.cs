using Generator.Domain.Abstractions;
using System;

namespace Generator.Domain.Entities
{
    public class UserChoice : Entity
    {
        public UserChoice(Guid chosenPictureId, Guid otherPictureId)
        {
            ChosenPictureId = chosenPictureId;
            OtherPictureId = otherPictureId;
        }
        public Guid ChosenPictureId { get; private set; }

        public Guid OtherPictureId { get; private set; }
    }
}
