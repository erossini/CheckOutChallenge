using Basket.WebApi.Models;
using Basket.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger _logger;
        private readonly BasketContext _context;

        public ProductController(ILogger<ProductController> logger, BasketContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return _context.Products;
        }
    }
}