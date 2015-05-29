

namespace MakersMarket.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models.Domain;
    internal sealed class Configuration : DbMigrationsConfiguration<MakersMarket.Models.ApplicationDbContext>
    {
        private PasswordHasher passwordHasher = new PasswordHasher();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MakersMarket.Models.ApplicationDbContext";
        }

        protected override void Seed(MakersMarket.Models.ApplicationDbContext context)
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

     
        }

        private IdentityRole CreateRole(MakersMarket.Models.ApplicationDbContext context, RoleManager<IdentityRole> roleManager, string roleName)
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

        private User CreateUser(MakersMarket.Models.ApplicationDbContext context, UserManager<User> userManager, string username, string password)
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
