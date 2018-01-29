using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Enums
{
    /// <summary>
    /// BasketError enum
    /// </summary>
    public enum BasketError
    {
        /// <summary>
        /// The model is not valid
        /// </summary>
        ModelIsNotValid = 100,

        /// <summary>
        /// The client identifier can't be null
        /// </summary>
        ClientIdCantBeNull = 101,

        /// <summary>
        /// The sku can't be null
        /// </summary>
        SKUCantBeNull = 102,

        /// <summary>
        /// The product not available
        /// </summary>
        ProductNotAvailable = 103,
    }
}
