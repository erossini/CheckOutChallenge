using Basket.DAL.Models.Requests;
using Basket.Library.Helpers;
using Basket.Library.Interfaces;
using Basket.WebApi.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library.Clients
{
    public sealed class TokenClient
    {
        static volatile TokenClient _instance;
        static object _syncRoot = new Object();

        public static TokenClient Instance
        {
            get {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new TokenClient();
                    }
                }

                return _instance;
            }
        }

        public TokenResponse Token { get; set; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="tokenRequest">Token request.</param>
        public async Task GetToken(TokenRequest tokenRequest)
        {
            if (Token == null || DateTime.Now > Token.ExpiredDate)
            {
                var basketApi = RestService.For<IBasket>(UrlHelpers.BaseUrl);
                Token = await basketApi.Create(new TokenRequest(tokenRequest.Username, tokenRequest.Password));
            }
        }
    }
}