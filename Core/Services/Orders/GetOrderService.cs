using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Services.Products;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IProductRepository _productRepository;
        
        public GetOrderService(IOrderRepository OrderRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _OrderRepository = OrderRepository;    

        }

        public Order GetOrder(Guid id)
        {
            var order = _OrderRepository.Get(id);

            var prods = getProductsByOrderId(id);
            order.SetProducts(prods);
            
            var total = GetTotal(id);
            order.SetTotal(total);         

            return order;
        }

        public IEnumerable<Order> GetOrders(string name = null)
        {
            return _OrderRepository.Get(name);
        }

        private IEnumerable<Product> getProductsByOrderId(Guid Id)
        {
            var order = _OrderRepository.Get(Id);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id {Id} does not exist.");
            }

            var productList = new List<Product>();

            foreach (var item in order.LineItems)
            {
                Guid guid = Guid.TryParse(item.ProductId, out var parsedGuid) ? parsedGuid : Guid.Empty;
                if (guid == Guid.Empty)
                {
                    throw new InvalidOperationException($"Item {item.ProductId} is not a valid product ID.");
                }
                Product prod = _productRepository.Get(guid);
                productList.Add(prod);
            }

            return productList;

        }

        public decimal? GetTotal(Guid Id)
        {
            decimal? total = 0.00M;
            var order = _OrderRepository.Get(Id);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id {Id} does not exist.");
            }

            foreach(var item in order.LineItems)
            {
                Guid guid = Guid.TryParse(item.ProductId, out var parsedGuid) ? parsedGuid : Guid.Empty;
                if (guid == Guid.Empty)
                {
                    throw new InvalidOperationException($"Item {item.ProductId} is not a valid product ID.");
                }
                Product prod = _productRepository.Get(guid);
                total += item.Quantity * prod.Price;
            }
          

            return total;
        }
      
    }
}