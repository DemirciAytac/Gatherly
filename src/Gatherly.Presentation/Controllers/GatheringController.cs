using Gatherly.Application.Gatherings.Commands.CreateGathering;
using Gatherly.Application.Gatherings.Queries.GetGatheringById;
using Gatherly.Presentation.Absractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Gatherly.Presentation.Controllers
{
    [Route("api/gathering")]
    public sealed class GatheringController : ApiController
    {
        public GatheringController(ISender sender):base(sender)
        {
                
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGatheringById(Guid id)
        {
            var query = new GetGatheringByIdQuery(id);

            var response = await Sender.Send(query);

            return Ok(response);
        }
            
    }
}
