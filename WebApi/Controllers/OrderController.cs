using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Orders;
using WebApi.Models.Orders;




namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(IGetOrderService getOrderService,
            IDeleteOrderService deleteOrderService,
            IUpdateOrderService updateOrderService, ICreateOrderService createOrderService)
        {  
            _getOrderService = getOrderService;           
            _createOrderService  = createOrderService;
            _updateOrderService = updateOrderService;
            _deleteOrderService = deleteOrderService;
        }
               
        [Route("{OrderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid OrderId, [FromBody] OrderModel model)
        {
            var Order = _createOrderService.Create(OrderId, model.Name, model.LineItems);
            var orderData = _getOrderService.GetOrder(OrderId);
            return Found(new OrderData(orderData));
        }

        [Route("{OrderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid OrderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(OrderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _updateOrderService.Update(order, model.Name, model.LineItems);
            return Found(new OrderData(order));
        }

        [Route("{OrderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid OrderId)
        {
            var order = _getOrderService.GetOrder(OrderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.Delete(order);
            return Found();
        }

        [Route("{OrderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid OrderId)
        {
            var order = _getOrderService.GetOrder(OrderId);
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, string name = null)
        {
            var orders = _getOrderService.GetOrders(name)
                                       .Skip(skip).Take(take)
                                       .Select(q => new OrderData(q))
                                       .ToList();
            return Found(orders);
        }

       
    }
}