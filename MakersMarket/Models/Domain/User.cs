using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MakersMarket.Models.Domain
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Shops = new HashSet<Shop>();
        }
        public ICollection<Shop> Shops{ get; set; }
    }
}