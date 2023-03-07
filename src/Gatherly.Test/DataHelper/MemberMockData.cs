﻿using Gatherly.Application.Members.Queries.GetMemberById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Test.DataHelper
{
    public class MemberMockData
    {
        public static MemberResponse GetFakeMemberResponse(Guid id)
        {
            return new MemberResponse(id, "aytac.demirci92@gmail.com");
        }
    }
}
