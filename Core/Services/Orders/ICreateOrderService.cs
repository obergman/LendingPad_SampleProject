﻿using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, string name, IEnumerable<LineItem> lineItems);
    }
}