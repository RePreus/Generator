using System.Runtime.InteropServices.ComTypes;
using Generator.Application.Models;
using MediatR;

namespace Generator.Application.DTOs
{
    /// <summary>
    /// DTO of Payload object.
    /// </summary>
    public class PayloadDto : IRequest<RequestResult>
    {
        public int Grade { get; set; }

        public string Hash { get; set; }

        public string Url { get; set; }
    }
}
