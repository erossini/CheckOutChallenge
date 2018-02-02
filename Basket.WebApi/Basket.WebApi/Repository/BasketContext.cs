using Basket.DAL.Models;
using Basket.DAL.Requests;
using Microsoft.EntityFrameworkCore;

namespace Basket.WebApi.Repository
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options)
            : base(options)
        {
        }

        public DbSet<BasketRequest> Baskets { get; set; }

        public DbSet<ProductModel> Products { get; set; }
    }
}
