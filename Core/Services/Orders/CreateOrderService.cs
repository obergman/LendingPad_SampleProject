using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{


    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _OrderFactory;
        private readonly IOrderRepository _OrderRepository;

        public CreateOrderService(IIdObjectFactory<Order> OrderFactory, IOrderRepository OrderRepository, IUpdateOrderService updateOrderService)
        {
            _OrderFactory = OrderFactory;
            _OrderRepository = OrderRepository;
            _updateOrderService = updateOrderService;
        }      

        public Order Create(Guid id, string name, IEnumerable<LineItem> lineItems)
        {
            // check if user id exists
            if (_OrderRepository.Get(id) != null)
            {
                throw new InvalidOperationException($"Order with id {id} already exists.");
            }

            var Order = _OrderFactory.Create(id);

            _updateOrderService.Update(Order, name, lineItems);      
            _OrderRepository.Save(Order);
            
            return Order;
        }
    }
}