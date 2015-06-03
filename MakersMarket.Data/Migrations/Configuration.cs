namespace MakersMarket.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<MakersMarket.Data.MakersMarketDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MakersMarketDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            // Seed roles
            var adminRole = CreateRole(context, roleManager, "AppAdmin");

            // Seed users
            var user = CreateUser(context, userManager, "user", "123456");

            var admin = CreateUser(context, userManager, "admin", "123456");
            userManager.AddToRole(admin.Id, adminRole.Name);

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
        private IdentityRole CreateRole(MakersMarketDbContext context, RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (context.Roles.Any(r => r.Name == roleName))
            {
                return context.Roles.First(r => r.Name == roleName);
            }
            else
            {
                var role = new IdentityRole { Name = roleName };

                roleManager.Create(role);

                return role;
            }
        }
     private User CreateUser(MakersMarketDbContext context, UserManager<User> userManager, string username, string password)
        {
            if (context.Users.Any(u => u.UserName == username))
            {
                return context.Users.First(u => u.UserName == username);
            }
            else
            {
                var user = new User { UserName = username };

                userManager.Create(user, password);

                return user;
            }
        }
    }
}
