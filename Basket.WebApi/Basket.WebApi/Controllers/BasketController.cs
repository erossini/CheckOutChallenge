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
        private readonly BasketContext _context;

        public BasketController(BasketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<BasketModel> Get(string clientId)
        {
            if (_context.Baskets == null)
                return null;

            return _context.Baskets.Where(b => b.ClientId == clientId).ToList();
        }

        [HttpPost]
        public IActionResult Add([FromBody]BasketModel model)
        {
            BasketResponse response = new BasketResponse();
            response.Error = BasketError.ModelIsNotValid;
            response.Success = false;

            if (ModelState.IsValid) {
                var validationResult = model.Validate();
                if (validationResult.IsValid)
                {
                    response.Error = new BasketOperations(_context).AddOrUpdateBasketItem(model);
                    if (response.Error == BasketError.NoError)
                        return Ok();
                    else
                        return BadRequest(response);
                }
            }

            return BadRequest(response);
        }

        [HttpDelete]
        public IActionResult DeleteItem([FromBody] DeleteItemRequest request)
        {
            BasketResponse response = new BasketResponse();
            response.Error = BasketError.ModelIsNotValid;
            response.Success = false;

            if (ModelState.IsValid)
            {
                var validationResult = request.Validate();
                if (validationResult.IsValid)
                {
                    new BasketOperations(_context).DeleteItem(request);
                    return Ok();
                }
            }

            return BadRequest(response);
        }

        /// <summary>
        /// Empties the basket.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete]
        public IActionResult EmptyBasket([FromBody] string clientId)
        {
            if (!string.IsNullOrEmpty(clientId))
            {
                new BasketOperations(_context).EmptyBasket(clientId);
                return Ok();
            }
            else
            {
                BasketResponse response = new BasketResponse();
                response.Error = BasketError.ClientIdCantBeNull;
                response.Success = false;

                return BadRequest(response);
            }
        }
    }
}