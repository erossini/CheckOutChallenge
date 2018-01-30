using Basket.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Repository
{
    public static class ProductDataSeed
    {
        public static void AddProductData(BasketContext context)
        {
            List<ProductModel> products = new List<ProductModel>() {
                new ProductModel() { SKU = "A111", Description = "Surface Phone", Price = 100, Quantity = 100 },
                new ProductModel() { SKU = "A112", Description = "Surface Pro 1", Price = 120, Quantity = 200 },
                new ProductModel() { SKU = "A113", Description = "Surface Pro 2", Price = 140, Quantity = 200 },
                new ProductModel() { SKU = "P111", Description = "Windows10 Pro", Price = 123, Quantity = 600 },
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}