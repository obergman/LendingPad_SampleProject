using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrder(Guid id);

        decimal? GetTotal(Guid id);

        IEnumerable<Order> GetOrders(string name = null);
    }
}