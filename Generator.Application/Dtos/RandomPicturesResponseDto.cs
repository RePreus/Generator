using System;
using Generator.Domain.Entities;

namespace Generator.Application.Dtos
{
    public class RandomPicturesResponseDto
    {
        public RandomPicturesResponseDto(Picture pictureA, Picture pictureB)
        {
            this.PictureA = pictureA;
            this.PictureB = pictureB;
        }

        public RandomPicturesResponseDto(Guid pictureAId, string pictureA, Guid pictureBId, string pictureB)
            : this(new Picture(pictureAId, pictureA), new Picture(pictureBId, pictureB))
        {
        }

        public Picture PictureA { get; }

        public Picture PictureB { get; }
    }
}
