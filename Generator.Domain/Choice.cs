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
        public Guid PictureA { get; private set; }

        public Guid PictureB { get; private set; }

        public Guid UserChoice { get; private set; }
    }
}
