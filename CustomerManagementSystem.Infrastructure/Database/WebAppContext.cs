using CustomerManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Infrastructure.Database
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
