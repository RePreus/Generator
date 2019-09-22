using System;
using Generator.Domain.Entities;

namespace Generator.Application.Models
{
    public class PicturesPayload
    {
        public PicturesPayload(Picture pictureA, Picture pictureB)
        {
            this.PictureA = pictureA;
            this.PictureB = pictureB;
        }

        public PicturesPayload(Guid pictureAId, string pictureA, Guid pictureBId, string pictureB)
            : this(new Picture(pictureAId, pictureA), new Picture(pictureBId, pictureB))
        {
        }

        public Picture PictureA { get; }

        public Picture PictureB { get; }
    }
}
