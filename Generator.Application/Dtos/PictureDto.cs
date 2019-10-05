using System;

namespace Generator.Application.Dtos
{
    public class PictureDto
    {
        public PictureDto(Guid id, string image)
        {
            Id = id;
            Image = image;
        }

        public Guid Id { get; }

        public string Image { get; }
    }
}
