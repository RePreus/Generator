using System.Collections.Generic;

namespace Generator.Application.Dtos
{
    public class RandomPicturesResponseDto
    {
        public RandomPicturesResponseDto(PictureDto pictureDtoA, PictureDto pictureDtoB)
        {
            Pictures = new List<PictureDto>() { pictureDtoA, pictureDtoB };
        }

        public List<PictureDto> Pictures { get; }
    }
}