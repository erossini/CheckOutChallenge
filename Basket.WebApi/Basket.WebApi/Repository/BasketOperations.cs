using Basket.WebApi.Enums;
using Basket.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Repository
{
    public class BasketOperations
    {
        BasketContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasketOperations"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BasketOperations(BasketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the or update basket item.
        /// </summary>
        /// <param name="basketItem">The basket item.</param>
        /// <returns>BasketError.</returns>
        public BasketError AddOrUpdateBasketItem(BasketModel basketItem)
        {
            BasketError rtn = BasketError.ProductNotAvailable;

            ProductOperations po = new ProductOperations(_context);
            if (po.IsAvailable(basketItem.SKU, basketItem.Quantity))
            {
                Tuple<bool, decimal> update = po.UpdateQuantity(basketItem.SKU, basketItem.Quantity);
                List<BasketModel> list = _context.Baskets.Where(b => b.ClientId.Trim().ToLower() == basketItem.ClientId.Trim().ToLower() &&
                                                                 b.SKU.Trim().ToLower() == basketItem.SKU.Trim().ToLower()).ToList();
                if (list.Count() > 0)
                {
                    if (list.Count == 1)
                    {
                        list.SingleOrDefault().Quantity += basketItem.Quantity;
                        list.SingleOrDefault().TotalPrice = list.SingleOrDefault().Quantity * update.Item2;
                        _context.SaveChanges();
                        rtn = BasketError.NoError;
                    }
                    else
                        rtn = BasketError.DuplicatedSKU;
                }
                else
                {
                    if (update.Item1)
                    {
                        basketItem.TotalPrice += basketItem.Quantity * update.Item2;
                        _context.Baskets.Add(basketItem);
                        _context.SaveChanges();

                        rtn = BasketError.NoError;
                    }
                }
            }

            return rtn;
        }
    }
}