using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.DAL.Models.Requests
{
    /// <summary>
    /// Token request.
    /// </summary>
    public class TokenRequest
    {
        public TokenRequest() { }

        public TokenRequest(string username, string password)
        {
            Username = username;
            Password = password;
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
    }
}
