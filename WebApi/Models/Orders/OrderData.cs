using BusinessEntities;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order Order) : base(Order)
        {           
            Name = Order.Name;  
            LineItems = Order.LineItems;
            TotalPrice = Order.Total;
            Products = Order.Products;

        }

        public string Name { get; set; }
        
        public decimal? TotalPrice { get; set; }
        public IEnumerable<Product> Products { get; }

        public IEnumerable<LineItem> LineItems { get; set; }

    }
}