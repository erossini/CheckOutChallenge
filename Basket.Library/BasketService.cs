using Basket.DAL.Models.Requests;
using Basket.DAL.Requests;
using Basket.DAL.Responses;
using Basket.Library.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.Library
{
    /// <summary>
    /// Class BasketService.
    /// </summary>
    public class BasketService
    {
        BasketClient bs;
        TokenClient tokenClient;

        public BasketService(string username, string password)
        {
            Username = username;
            Password = password;

            tokenClient = TokenClient.Instance;
            bs = BasketClient.Instance;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        public string Token {
            get {
                return tokenClient.Token.Token;
            }
        }

        public string AuthorizationToken
        {
            get {
                return "Bearer " + tokenClient.Token.Token;
            }
        }

        /// <summary>
        /// Gets the token expired date.
        /// </summary>
        /// <value>The token expired date.</value>
        public DateTimeOffset? TokenExpiredDate
        {
            get {
                return tokenClient.Token.ExpiredDate;
            }
        }

        public async Task<bool> AddItem(BasketRequest request)
        {
            try
            {
                IAsyncResult result = await bs.AddItem(request, AuthorizationToken);
                return true;
            }
            catch(Exception ex)
            {
            }
            return false;
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task CreateToken()
        {
            await tokenClient.GetToken(new TokenRequest(Username, Password));
        }

        /// <summary>
        /// Gets the basket.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Task&lt;List&lt;BasketResponse&gt;&gt;.</returns>
        public async Task<List<BasketRequest>> GetBasket(string clientId)
        {
            try
            {
                return await bs.GetBasket(clientId, AuthorizationToken);
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}