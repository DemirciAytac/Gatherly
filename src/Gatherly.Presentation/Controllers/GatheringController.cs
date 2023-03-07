using Gatherly.Application.Gatherings.Commands.CreateGathering;
using Gatherly.Application.Gatherings.Queries.GetGatheringById;
using Gatherly.Presentation.Absractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Gatherly.Presentation.Controllers
{
    [Route("api/gathering")]
    internal class GatheringController : ApiController
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

        [HttpPost]
        public async Task<IActionResult> CreateGathering()
        {
            var inset = İnsertDumpData();
            foreach (var item in inset)
            {

            }
            return Ok();
        }
        public IEnumerable<CreateGatheringCommand> İnsertDumpData()
        {

            yield return new CreateGatheringCommand(new Guid(), Domain.Enums.GatheringType.WithExpirationForInvitations, DateTime.Now, "das", "fdfs", 5, 5);
        }
            
    }
}
