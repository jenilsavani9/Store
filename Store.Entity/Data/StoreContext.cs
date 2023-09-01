using Microsoft.EntityFrameworkCore;
using Store.Entity.Models;

namespace Store.Entity.Data
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UserStore> UserStores { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<StoreFeature> StoreFeatures { get; set; }
        public DbSet<StoreFile> StoreFiles { get; set; }
    }
}
