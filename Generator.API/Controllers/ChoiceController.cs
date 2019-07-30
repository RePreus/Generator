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
    public class ChoiceController : ControllerBase
    {
        private readonly IMediator mediator;

        public ChoiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChoiceDto request)
        {
           RequestResult requestResult = await mediator.Send(request);
           if (requestResult.Success)
           {
               return StatusCode(200);
           }
           else
           {
               return StatusCode(400, requestResult.Message);
           }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(403);
        }
    }
}
