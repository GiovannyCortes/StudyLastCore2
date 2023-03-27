using Microsoft.EntityFrameworkCore;
using StudyLastCore2.Models;

namespace StudyLastCore2.Data {

    public class StoreContext : DbContext {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrdersItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
