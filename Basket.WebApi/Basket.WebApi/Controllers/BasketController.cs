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
                    response.Error = new BasketOperations(_context).AddOrUpdateBasketItem(model);
                    if (response.Error == BasketError.NoError)
                        return Ok();
                    else
                        return BadRequest(response);
                }
            }

            return BadRequest(response);
        }
    }
}