//using System.Threading.Tasks;
//using Generator.Application.DTOs;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Generator.Application.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {
//        private readonly IMediator mediator;

//        public ValuesController(IMediator mediator)
//        {
//            this.mediator = mediator;
//        }

//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status200OK)]

//        public async Task<IActionResult> Post([FromBody] PayloadDto request)
//                => await this.mediator.Send(request);

//        [HttpGet]
//        public string Get()
//        {
//            return "kxdxdd";
//        }
//    }
//}
