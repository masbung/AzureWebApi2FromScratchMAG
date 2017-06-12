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
        // GET api/products        
        [SwaggerOperation("GetAllProducts")]
        public IEnumerable<Product> Get() {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            return dbContext.Products.ToArray();
        }
      
        // POST api/products
        [SwaggerOperation("AddProduct")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public IHttpActionResult Post([FromBody]Product product) {
            try {
                ApplicationDbContext dbContext = new ApplicationDbContext();                
                dbContext.Products.Add(product);
                dbContext.SaveChanges();                
                return Created(new Uri(Request.RequestUri + product.ProductId.ToString()), product);
            } catch {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
                return ResponseMessage(response);
            }
        }       
    }
}