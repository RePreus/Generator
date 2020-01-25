using System.Collections.Generic;

namespace Generator.Application.Dtos
{
    public class RandomPicturesResponseDto
    {
        public RandomPicturesResponseDto(PictureDto pictureDtoA, PictureDto pictureDtoB, string token)
        {
            Pictures = new List<PictureDto>() { pictureDtoA, pictureDtoB };
            Token = token;
        }

        public List<PictureDto> Pictures { get; }

        public string Token { get; }
    }
}