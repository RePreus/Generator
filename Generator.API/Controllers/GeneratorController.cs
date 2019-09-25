using System.Threading.Tasks;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Generator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        private readonly IMediator mediator;

        public GeneratorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("pictures")]
        public async Task<IActionResult> Post([FromBody] SaveChosenPicturesCommand request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpGet("pictures")]
        public async Task<ActionResult<RandomPicturesResponseDto>> Get([FromBody] GetRandomPicturesQuery receivedName)
            => await mediator.Send(receivedName);
    }
}
