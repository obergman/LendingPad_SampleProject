using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public User CustomerId { get; set; }
        public string BillingAddress { get; set; }

        private string _name;
        private decimal? _total;



        // line items
        private readonly List<LineItem> _lineItems = new List<LineItem>();

        public IEnumerable<LineItem> LineItems
        {
            get => _lineItems;
            private set => _lineItems.Initialize(value);
        }

        public void SetLineItems(IEnumerable<LineItem> lines)
        {
            _lineItems.Initialize(lines);
        }

        // products
        public IEnumerable<Product> Products
        {
            get => _products;
            private set => _products.Initialize(value);
        }

        private readonly List<Product> _products = new List<Product>();

        public void SetProducts(IEnumerable<Product> prods)
        {
            _products.Initialize(prods);
        }


        // total price
        public decimal? Total
        {
            get => _total;
            private set => _total = value;

        }

        public void SetTotal(decimal? total)
        {
            if (total == null)
            {
                throw new ArgumentNullException("Total was not provided.");
            }
            _total = total;
        }


        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            _name = name;
        }


    }


}