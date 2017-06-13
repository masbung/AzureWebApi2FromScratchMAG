using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureWebApi2FromScratch.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }   
    }

    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Get(int productId);
        bool AddProduct(Product product);
    }

    //Database
    public class DbProductService : IProductService
    {

        public IEnumerable<Product> GetAll() {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            return dbContext.Products.ToArray();
        }

        public Product Get(int productId) {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            return dbContext.Products.Find(productId);
        }

        public bool AddProduct(Product product) {
            try {
                ApplicationDbContext dbContext = new ApplicationDbContext();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return true;
            } catch {                
                return false;
            }
        }
    }


    public class MemoryProductService : IProductService
    {
        private static Dictionary<int, Product> products;

        static MemoryProductService() {
            MemoryProductService.products = new Dictionary<int, Product>();
        }

        public IEnumerable<Product> GetAll() {
            return MemoryProductService.products.Values.ToArray();            
        }

        public Product Get(int productId) {
            return MemoryProductService.products[productId];
        }

        public bool AddProduct(Product product) {
            MemoryProductService.products.Add(product.ProductId, product);
            return true;
        }
    }

    public class ProductServiceFactory
    {
        public static IProductService CreateProductService() {
            return new MemoryProductService();
        }
    }
}