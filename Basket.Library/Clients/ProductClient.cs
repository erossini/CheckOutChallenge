using Basket.DAL.Models;
using Basket.Library.Helpers;
using Basket.Library.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library.Clients
{
    public class ProductClient
    {
        static volatile ProductClient _instance;
        static object _syncRoot = new Object();

        public static ProductClient Instance
        {
            get {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new ProductClient();
                    }
                }

                return _instance;
            }
        }

        public async Task<List<ProductModel>> GetProducts(string token)
        {
            var basketApi = RestService.For<IBasket>(UrlHelpers.BaseUrl);
            return await basketApi.GetProductList(token);
        }
    }
}
