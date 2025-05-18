using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order user, string name, IEnumerable<LineItem> lineItems);
    }
}