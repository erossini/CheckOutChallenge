using Basket.DAL.Models;
using Basket.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Repository
{
    /// <summary>
    /// Class ProductOperations.
    /// </summary>
    public class ProductOperations
    {
        BasketContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOperations"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProductOperations(BasketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Determines whether the specified sku is available.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns><c>true</c> if the specified sku is available; otherwise, <c>false</c>.</returns>
        public bool IsAvailable(string sku, int quantity)
        {
            return _context.Products.Where(p => p.SKU.Trim() == sku.Trim() && p.Quantity >= quantity).Count() >= 1;
        }

        /// <summary>
        /// Updates the quantity.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>Tuple&lt;System.Boolean, System.Decimal&gt;.</returns>
        public Tuple<bool, decimal> UpdateQuantity(string sku, int quantity)
        {
            bool rtn = false;
            decimal tot = -1;

            if (IsAvailable(sku, quantity))
            {
                ProductModel prod = _context.Products.Where(p => p.SKU.Trim() == sku.Trim()).FirstOrDefault();
                if (prod != null)
                {
                    prod.Quantity -= quantity;
                    _context.SaveChanges();

                    tot = quantity * prod.Price;
                    rtn = true;
                }
            }

            return new Tuple<bool, decimal>(rtn, tot);
        }
    }
}
