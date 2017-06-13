using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using AzureWebApi2FromScratch.Models;

namespace AzureWebApi2FromScratch.Controllers
{
    public class ProductsController : ApiController
    {
        IProductService service;

        public ProductsController() {
            service = ProductServiceFactory.CreateProductService();            
        }

        //public ProductsController(IProductService service) {            
        //    this.service = service;
        //}

        // GET api/products        
        [SwaggerOperation("GetAllProducts")]
        public IEnumerable<Product> Get() {
            //IProductService service = new DbProductService();            
            return service.GetAll();
        }

        // GET api/products        
        [SwaggerOperation("GetProduct")]
        public Product Get(int productId) {
            //IProductService service = new DbProductService();
            //IProductService service = ProductServiceFactory.CreateProductService();
            return service.Get(productId);            
        }
      
        // POST api/products
        [SwaggerOperation("AddProduct")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public IHttpActionResult Post([FromBody]Product product) {
            //try {
            //    ApplicationDbContext dbContext = new ApplicationDbContext();                
            //    dbContext.Products.Add(product);
            //    dbContext.SaveChanges();                
            //    return Created(new Uri(Request.RequestUri + product.ProductId.ToString()), product);
            //} catch {
            //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
            //    return ResponseMessage(response);
            //}


            //IProductService service = new DbProductService();            
            if (service.AddProduct(product)) {
                return Created(new Uri(Request.RequestUri + product.ProductId.ToString()), product);
            } else {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
                return ResponseMessage(response);
            }

        }       
    }
}