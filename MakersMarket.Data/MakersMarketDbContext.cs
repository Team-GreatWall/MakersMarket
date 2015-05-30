namespace MakersMarket.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Migrations;
    
    public class MakersMarketDbContext:IdentityDbContext<User>,IMakersMarketDbContext
    {
        public MakersMarketDbContext() 
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MakersMarketDbContext,Configuration>());
        }

        public static MakersMarketDbContext Create()
        {
            return new MakersMarketDbContext();
        }
        public IDbSet<Brand> Brands { get; set; }
        public IDbSet<CartItem> CartItems { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Image> Images { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Shop> Shops { get; set; }
    }
}
