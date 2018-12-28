using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RequestManagement;
using SampleApiWebApp.Constants;
using SampleApiWebApp.Models;
using SampleApiWebApp.Models.Requests;

namespace SampleApiWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TeamsController : Controller
    {
        public TeamsController(IMediator mediator)
        {
            Mediator = mediator;
        }

        private IMediator Mediator { get; }

        [HttpPost]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post(
            [FromBody]PostTeamRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var result = await Mediator.Send(request, cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet]
        [Route("{id}")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200, Type = typeof(Team))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOne([FromRoute]long id, CancellationToken cancellationToken)
        {
            var request = new GetTeamRequest { Id = id };
            var result = await Mediator.Send(request, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPut]
        [Route("{id}")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(
            [FromRoute]long id,
            [FromBody]PutTeamRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            request.Id = id;

            var result = await Mediator.Send(request, cancellationToken);

            return result.ToActionResult();
        }
    }
}
