using Generator.Domain;

namespace Generator.Application.Models
{
    public class Payload
    {
        public Payload(Picture pictureA, Picture pictureB)
        {
            this.PictureA = pictureA;
            this.PictureB = pictureB;
        }

        public Payload(int pictureAId, string pictureA, int pictureBId, string pictureB)
            : this(new Picture(pictureAId, pictureA), new Picture(pictureBId, pictureB))
        {
        }

        public Picture PictureA { get; }

        public Picture PictureB { get; }
    }
}
