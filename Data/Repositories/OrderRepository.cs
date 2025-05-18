using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{


    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class OrderRepository : InMemoryRepository<Order>, IOrderRepository
    {
        public OrderRepository() : base()
        {
        }
             

        public IEnumerable<Order> Get(string name)
        {
            var query = GetAll();

            if (name != null)
            {
                query = query.Where(x => x.Name == name);
            }

            return query;


        }
    }

}