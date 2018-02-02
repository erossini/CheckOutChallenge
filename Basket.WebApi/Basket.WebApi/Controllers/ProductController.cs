using Basket.DAL.Models;
using Basket.WebApi.Models;
using Basket.WebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly BasketContext _context;

        public ProductController(BasketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return _context.Products;
        }
    }
}