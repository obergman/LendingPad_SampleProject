using System.Collections.Generic;
using BusinessEntities;
using Common;
using SimpleInjector;

namespace Core.Services.Products
{
    [AutoRegister]
    public class UpdateProductService : IUpdateProductService
    {       

        public void Update(Product product, string name, string description, decimal price)
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);
        }
    }
}