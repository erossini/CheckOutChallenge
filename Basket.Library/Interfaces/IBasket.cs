using Basket.DAL.Models;
using Basket.DAL.Models.Requests;
using Basket.DAL.Requests;
using Basket.DAL.Responses;
using Basket.WebApi.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Library.Interfaces
{
    public interface IBasket
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="tokenRequest">Token request.</param>
        [Post("/token")]
        Task<TokenResponse> Create(TokenRequest tokenRequest);

        [Get("/product")]
        Task<List<ProductModel>> GetProductList([Header("Authorization")] string token);

        [Post("/basket/add")]
        Task<IAsyncResult> AddItemToBasket(BasketRequest request, [Header("Authorization")] string token);

        [Get("/basket/get?clientId={clientId}")]
        Task<List<BasketRequest>> GetBasket(string clientId, [Header("Authorization")] string token);

        [Delete("/basket/")]
        Task<IAsyncResult> EmptyBasket(string clientId, [Header("Authorization")] string token);
    }
}