using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister] 
    public class UpdateOrderervice : IUpdateOrderService
    {       

        public void Update(Order order, string name, IEnumerable<LineItem> lineItems)
        {
            order.SetName(name);
            order.SetLineItems(lineItems);            
        }
    }
}