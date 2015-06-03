namespace MakersMarket.Data
{
    using System.Data.Entity;
    using Models;
    public interface IMakersMarketDbContext
    {
        IDbSet<Brand> Brands { get; set; }

        IDbSet<CartItem> CartItems { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<ImageShop> ImagesShop { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<Shop> Shops { get; set; }

        IDbSet<User> Users { get; set; }

    }
}
