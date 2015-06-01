namespace MakersMarket.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MakersMarket.Data.MakersMarketDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MakersMarketDbContext context)
        {
            // Initialize Categories
            if (context.Categories.Count() <= 0 || context.Categories == null)
            {
                Category electronics = new Category()
                {
                    Name = "Electronics",
                    Description = "Items used to affect the electrons or their associated fields in a manner consistent with the intended function of the electronic system."
                };
                context.Categories.Add(electronics);
                Category clothes = new Category()
                {
                    Name = "Clothes",
                    Description = "Item made from textile or cloth used to wear."
                };
                context.Categories.Add(clothes);
                Category jewellery = new Category()
                {
                    Name = "Jewellery",
                    Description = "Decorative item used to wear."
                };
                context.Categories.Add(jewellery);
                Category ceramics = new Category()
                {
                    Name = "Ceramics",
                    Description = "Item made from ceramic material."
                };
                context.Categories.Add(ceramics);
                Category woodCraft = new Category()
                {
                    Name = "Wood Craft",
                    Description = "Item made from wood."
                };
                context.Categories.Add(woodCraft);
                Category interior = new Category()
                {
                    Name = "Inerior",
                    Description = "Item for interior decoration."
                };
                context.Categories.Add(interior);
                context.SaveChanges();
            }

            // Brand Initialization
            if (context.Brands.Count() <= 0 || context.Brands == null)
            {
                Brand selfMade = new Brand()
                {
                    Name = "Self Made",
                    Description = "Item made by shop owner."
                };
                context.Brands.Add(selfMade);
                Brand cooperative = new Brand()
                {
                    Name = "Cooperative",
                    Description = "Item made by cooperative group of people."
                };
                context.Brands.Add(cooperative);
                context.SaveChanges();
            }

        }
    }
}
