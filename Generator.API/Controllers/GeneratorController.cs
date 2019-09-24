using System.Threading.Tasks;
using Generator.Application.Commands;
using Generator.Application.Models;
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

        [HttpPost("response")]
        public async Task<IActionResult> Post([FromBody] SaveChosenPicturesCommand request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpGet("request")]
        public async Task<ActionResult<PicturesPayload>> Get([FromBody] GetRandomPicturesQuery receivedName)
            => await mediator.Send(receivedName);
    }
}
