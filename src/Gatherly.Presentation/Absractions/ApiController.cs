using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Presentation.Absractions
{

    /*
     Sadece command ve Query göndermek istiyorsak ISender interfacesini kullanmamız yeterlidir.
     Notification göndermek istiyorask IPublisher interfacesini kullanırız.    
     */
    public class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        public ApiController(ISender sender)
        {
            Sender = sender;
        }


    }

}
