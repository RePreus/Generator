using System;

namespace Generator.Domain
{
    public class Choice
    {
        public Choice(Guid pictureA, Guid pictureB, Guid userChoice)
        {
            PictureA = pictureA;
            PictureB = pictureB;
            UserChoice = userChoice;
        }
        public Guid PictureA { get; }

        public Guid PictureB { get; }

        public Guid UserChoice { get; }
    }
}
