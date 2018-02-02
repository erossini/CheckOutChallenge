using Basket.DAL.Enums;
using Basket.DAL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.WebApi.Repository
{
    public class BasketOperations
    {
        BasketContext _context;
        ProductOperations po;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasketOperations"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BasketOperations(BasketContext context)
        {
            _context = context;
            po = new ProductOperations(_context);
        }

        /// <summary>
        /// Adds the or update basket item.
        /// </summary>
        /// <param name="basketItem">The basket item.</param>
        /// <returns>BasketError.</returns>
        public BasketError AddOrUpdateBasketItem(BasketRequest basketItem)
        {
            BasketError rtn = BasketError.ProductNotAvailable;

            if (po.IsAvailable(basketItem.SKU, basketItem.Quantity))
            {
                Tuple<bool, decimal> update = po.UpdateQuantity(basketItem.SKU, basketItem.Quantity);
                List<BasketRequest> list = _context.Baskets.Where(b => b.ClientId.Trim().ToLower() == basketItem.ClientId.Trim().ToLower() &&
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

        /// <summary>
        /// Deletes the items.
        /// </summary>
        /// <param name="list">The list.</param>
        private void DeleteItems(IEnumerable<BasketRequest> list)
        {
            foreach (BasketRequest item in list)
                po.UpdateQuantity(item.SKU, -item.Quantity);

            _context.Baskets.RemoveRange(list);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="request">The request.</param>
        public void DeleteItem(DeleteItemRequest request)
        {
            var list = _context.Baskets.Where(b => b.ClientId == request.ClientId && b.SKU == request.SKU);
            DeleteItems(list);
        }

        /// <summary>
        /// Empties the basket.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        public void EmptyBasket(string clientId)
        {
            var list = _context.Baskets.Where(b => b.ClientId == clientId);
            DeleteItems(list);
        }
    }
}