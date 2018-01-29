using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.WebApi.Enums;
using Basket.WebApi.Models;
using Basket.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Basket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BasketController : Controller
    {
        private readonly ILogger _logger;
        private readonly BasketContext _context;

        public BasketController(ILogger<BasketController> logger, BasketContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BasketModel> Get(string clientId)
        {
            if (_context.Baskets != null)
                return null;

            return _context.Baskets.Where(b => b.ClientId == clientId);
        }

        [HttpPost]
        public IActionResult Add([FromBody]BasketModel model)
        {
            BasketResponse response = new BasketResponse();
            response.Success = false;

            if (!ModelState.IsValid)
            {
                response.Error = BasketError.ModelIsNotValid;
            }
            else
            {
                var validationResult = model.Validate();
                if (validationResult.IsValid)
                {
                    ProductOperations po = new ProductOperations(_context);
                    if (po.IsAvailable(model.SKU, model.Quantity))
                    {
                        _context.Baskets.Add(model);
                        _context.SaveChanges();

                        po.UpdateQuantity(model.SKU, -model.Quantity);

                        return Ok();
                    }
                    else
                        response.Error = BasketError.ProductNotAvailable;
                }
            }

            return BadRequest(response);
        }
    }
}
