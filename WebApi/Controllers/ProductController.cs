using System;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Xml.Linq;
using BusinessEntities;
using Core.Services.Products;
using WebApi.Models.Products;




namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(IGetProductService getProductService,
            IDeleteProductService deleteProductService,
            IUpdateProductService updateProductService, ICreateProductService createProductService)
        {  
            _getProductService = getProductService;           
            _createProductService  = createProductService;
            _updateProductService = updateProductService;
            _deleteProductService = deleteProductService;
        }
               
        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _createProductService.Create(productId, model.Name, model.Description, model.Price);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var prod = _getProductService.GetProduct(productId);
            if (prod == null)
            {
                return DoesNotExist();
            }
            _updateProductService.Update(prod, model.Name, model.Description, model.Price);
            return Found(new ProductData(prod));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var prod = _getProductService.GetProduct(productId);
            if (prod == null)
            {
                return DoesNotExist();
            }
            _deleteProductService.Delete(prod);
            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, string name = null)
        {
            var prods = _getProductService.GetProducts(name)
                                       .Skip(skip).Take(take)
                                       .Select(q => new ProductData(q))
                                       .ToList();
            return Found(prods);
        }


    }
}