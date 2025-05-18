using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _OrderRepository;

        public DeleteOrderService(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }

        public void Delete(Order Order)
        {
            _OrderRepository.Delete(Order);
        }

       
    }
}