using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{


    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class ProductRepository : InMemoryRepository<Product>, IProductRepository
    {
        public ProductRepository() : base()
        {
        }

      
        public IEnumerable<Product> Get(string name)
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