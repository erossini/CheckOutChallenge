using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Models
{
    /// <summary>
    /// Class TokenResponse.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the expired date.
        /// </summary>
        /// <value>The expired date.</value>
        public DateTimeOffset ExpiredDate { get; set; }
    }
}
