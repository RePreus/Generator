using System.Threading.Tasks;
using Generator.Application.DTOs;
using Generator.Application.Models;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChoiceDto request)
        {
            await mediator.Send(request);
            return StatusCode(200);
        }

        [HttpGet]
        public async Task<ActionResult<Payload>> Get([FromBody] TableName tableName)
            => await mediator.Send(tableName);
    }
}
