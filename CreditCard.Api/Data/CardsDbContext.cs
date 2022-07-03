using CreditCard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.Api.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {

        }

        //DbSet
        public DbSet<Card> Cards { get; set; }
    }
}
