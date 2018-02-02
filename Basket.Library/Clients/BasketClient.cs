using Basket.DAL.Requests;
using Basket.DAL.Responses;
using Basket.Library.Helpers;
using Basket.Library.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library.Clients
{
    public class BasketClient
    {
        static volatile BasketClient _instance;
        static object _syncRoot = new Object();

        public static BasketClient Instance
        {
            get {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new BasketClient();
                    }
                }

                return _instance;
            }
        }

        public async Task<IAsyncResult> AddItem(BasketRequest request, string token)
        {
            var basketApi = RestService.For<IBasket>(UrlHelpers.BaseUrl);
            IAsyncResult result = await basketApi.AddItemToBasket(request, token);
            return result;
        }

        public async Task<List<BasketRequest>> GetBasket(string clientId, string token)
        {
            var basketApi = RestService.For<IBasket>(UrlHelpers.BaseUrl);
            return await basketApi.GetBasket(clientId, token);
        }
    }
}
