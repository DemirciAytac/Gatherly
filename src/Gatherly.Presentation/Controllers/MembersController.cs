using Gatherly.Application.Members.Commands.CreateMember;
using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Presentation.Absractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Presentation.Controllers
{
    [Route("api/members")]
    public sealed class MembersController: ApiController
    {
        public MembersController(ISender sender):base(sender)
        {

        }

        [HttpPost]
        public async  Task<IActionResult> RegisterMember()
        {
            var guid = Guid.NewGuid();
           var result =  await Sender.Send( new CreateMemberCommand("Hazal@gmail.com", "Hazal", "demirci"));
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMemberById(Guid id)
        {
            MemberResponse member = await Sender.Send(new GetMemberByIdQuery(id));

            return Ok(member);
        }
    }
}
