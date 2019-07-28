using System.Threading.Tasks;
using Generator.Application.DTOs;
using Generator.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Generator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ValuesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<RequestResult> Post([FromBody] PayloadDto request)
                => await mediator.Send(request);

        [HttpGet]
        public string Get()
        {
            return "kxdxdd";
        }
    }
}
