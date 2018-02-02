using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.DAL.Enums
{
    /// <summary>
    /// Enum BasketResult
    /// </summary>
    public enum BasketResult
    {
        /// <summary>
        /// The no change
        /// </summary>
        NoChange,

        /// <summary>
        /// The add
        /// </summary>
        Add,

        /// <summary>
        /// The update
        /// </summary>
        Update,

        /// <summary>
        /// The not in stock
        /// </summary>
        NotInStock,
    }
}
