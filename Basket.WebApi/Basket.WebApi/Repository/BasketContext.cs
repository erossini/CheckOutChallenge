using Basket.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.WebApi.Repository
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options)
            : base(options)
        {
        }

        public DbSet<BasketModel> Baskets { get; set; }

        public DbSet<ProductModel> Products { get; set; }
    }
}
