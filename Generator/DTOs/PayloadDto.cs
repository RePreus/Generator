using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Generator.Application.DTOs
{
    /// <summary>
    /// DTO of Payload object.
    /// </summary>
    public class PayloadDto : IRequest<IActionResult>
    {
        public int Grade { get; set; }

        public string Hash { get; set; }

        public string Url { get; set; }
    }
}
