using Generator.Application.Models;
using MediatR;

namespace Generator.Application.DTOs
{
    /// <summary>
    /// DTO of Choice object.
    /// </summary>
    public class ChoiceDto : IRequest
    {
        public int PictureAId { get; set; }

        public int PictureBId { get; set; }

        public int UserChoiceId { get; set; }
    }
}
