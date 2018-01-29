using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Repository
{
    public class ProductOperations
    {
        BasketContext _context;

        public ProductOperations(BasketContext context)
        {
            _context = context;
        }

        public bool IsAvailable(string sku, int quantity)
        {
            return _context.Products.Where(p => p.SKU.Trim() == sku.Trim() && p.Quantity >= quantity).Count() >= 1;
        }

        public bool UpdateQuantity(string sku, int quantity)
        {
            bool rtn = false;
            if (IsAvailable(sku, quantity))
            {
                _context.Products.Where(p => p.SKU.Trim() == sku.Trim()).FirstOrDefault().Quantity += quantity;
                _context.SaveChanges();
                rtn = true;
            }

            return rtn;
        }
    }
}
