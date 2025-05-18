using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string Name { get; set; }
        
        public IEnumerable<LineItem> LineItems { get; set; }

        public IEnumerable<Product> Products { get; set; }


    }
}