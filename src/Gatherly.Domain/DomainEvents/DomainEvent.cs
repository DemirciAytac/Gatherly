﻿using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.DomainEvents
{
    public abstract record DomainEvent(Guid Id) : IDomainEvent;

}
