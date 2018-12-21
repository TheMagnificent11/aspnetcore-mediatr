using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RequestManagement;
using SampleApiWebApp.Constants;
using SampleApiWebApp.Models;

namespace SampleApiWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TeamsController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamsController"/> class
        /// </summary>
        /// <param name="mediator">Mediator</param>
        public TeamsController(IMediator mediator)
        {
            Mediator = mediator;
        }

        private IMediator Mediator { get; }

        /// <summary>
        /// Registers a new team
        /// </summary>
        /// <param name="request">Create team request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// HTTP 200 if successful
        /// HTTP 400 if the post body contains validation errors
        /// </returns>
        [HttpPost]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post(
            [FromBody]CreateTeamRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var result = await Mediator.Send(request, cancellationToken);

            return result.ToActionResult();
        }
    }
}
