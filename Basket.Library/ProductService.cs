using Basket.DAL.Models;
using Basket.Library.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library
{
    public class ProductService
    {
        string _token;
        ProductClient client;

        public ProductService(string token)
        {
            _token = token;
            client = ProductClient.Instance;
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            return await client.GetProducts(_token);
        }
    }
}
