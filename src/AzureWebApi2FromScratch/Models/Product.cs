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
}