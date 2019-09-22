using System;

namespace Generator.Domain.Entities
{
    public class Choice
    {
        public Choice(Guid pictureAId, Guid pictureBId, Guid userChoiceId)
        {
            PictureAId = pictureAId;
            PictureBId = pictureBId;
            UserChoiceId = userChoiceId;
        }
        public Guid PictureAId { get; }

        public string PictureA { get; set; }

        public Guid PictureBId { get; }

        public string PictureB { get; set; }

        public Guid UserChoiceId { get; }
    }
}
