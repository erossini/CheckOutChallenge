using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.DAL.Enums
{
    /// <summary>
    /// BasketError enum
    /// </summary>
    public enum BasketError
    {
        /// <summary>
        /// The no error
        /// </summary>
        NoError = 0,

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

        /// <summary>
        /// The duplicated sku
        /// </summary>
        DuplicatedSKU = 104,
    }
}
