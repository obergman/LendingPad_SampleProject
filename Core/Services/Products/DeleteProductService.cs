using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductService(IProductRepository ProductRepository)
        {
            _productRepository = ProductRepository;
        }

        public void Delete(Product product)
        {
            _productRepository.Delete(product);
        }

       
    }
}