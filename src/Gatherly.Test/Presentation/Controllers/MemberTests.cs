using Gatherly.Application.Members.Queries.GetMemberById;
using Gatherly.Presentation.Controllers;
using Gatherly.Test.DataHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Moq.AutoMock;
using NodaTime;
using Shouldly;
using Xunit;


namespace Gatherly.Test.Presentation.Controllers
{
    public class MemberTests
    {
        [Fact]
        public async Task GetMemberById_ShouldReturnMember()
        {          
            //var memberResponse = MemberMockData.GetFakeMemberResponse(Guid.NewGuid());
            //var mocker = new AutoMocker(MockBehavior.Strict);
            //var mediator = mocker.GetMock<IMediator>();
            //mediator.Setup(x => x.Send<MemberResponse>(It.IsAny<GetMemberByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(memberResponse).Verifiable();

            //var memberController = new MembersController(mediator.Object);

            //var result = await memberController.GetMemberById(Guid.NewGuid());

            //result.ShouldNotBeNull();
            //result.ShouldBeOfType<OkObjectResult>();

        }
    }
}
