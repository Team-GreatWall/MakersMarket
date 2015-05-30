namespace MakersMarket.Data.Data
{
    using Repositories;
    using Models;

    public interface IMakersMarketData
    {
        IRepository<User> Users { get; }
        IRepository<Category> Categories { get; }
        IRepository<Brand> Brands { get; }
        IRepository<CartItem> CartItems { get; }
        IRepository<Image> Images { get; }
        IRepository<Product> Products { get; }
        IRepository<Shop> Shops { get; }
        int SaveChanges();
    }
}
