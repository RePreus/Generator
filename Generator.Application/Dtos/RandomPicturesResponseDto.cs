namespace Generator.Application.Dtos
{
    public class RandomPicturesResponseDto
    {
        public RandomPicturesResponseDto(PictureDto pictureDtoA, PictureDto pictureDtoB)
        {
            PictureDtoA = pictureDtoA;
            PictureDtoB = pictureDtoB;
        }

        public PictureDto PictureDtoA { get; }

        public PictureDto PictureDtoB { get; }
    }
}
