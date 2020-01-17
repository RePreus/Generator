using System.Linq;
using System.Threading.Tasks;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Exceptions;
using Generator.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Generator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PicturesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PicturesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SaveChosenPicturesCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<RandomPicturesResponseDto>> Get([FromQuery] GetRandomPicturesQuery query)
        {
            var sub = User.Claims.Where(c => c.Type == "sub").Select(c => c.Value).SingleOrDefault();
            if (string.IsNullOrWhiteSpace(sub))
                throw new GeneratorException("Missing 'sub' claim");
            return await mediator.Send(query);
        }
    }
}
