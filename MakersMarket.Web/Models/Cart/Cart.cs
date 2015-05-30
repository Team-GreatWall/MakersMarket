namespace MakersMarket.Web.Models.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Data.Data;
    using Data;
    using MakersMarket.Models;
    public class Cart
    {
        private const string SessionKey = "CartId";
        private string CartId { get; set; }
        
        private IMakersMarketData DB = new MakersMarketData(new MakersMarketDbContext());
        public static Cart GetCart(HttpContextBase context)
        {
            var cart = new Cart();
            cart.CartId = cart.GetCartId(context);
            return cart;
        }

        public static Cart GetCart(string userName)
        {
            var cart = new Cart();
            cart.CartId = userName;
            return cart;
        }

        public int Add(Product item)
        {
            var cartItem = DB.CartItems.All().SingleOrDefault(
                c => c.CartId == CartId
                && c.ProductId == item.Id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = item.Id,
                    CartId = CartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                DB.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            DB.SaveChanges();

            return cartItem.Quantity;
        }

        public int Remove(int id)
        {
            var cartItem = DB.CartItems.All().Single(
                cart => cart.CartId == CartId
                && cart.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    DB.CartItems.Delete(cartItem);
                }

                DB.SaveChanges();
            }
            return itemCount;
        }

        public void Clear()
        {
            var cartItems = DB.CartItems.All().Where(
                cart => cart.CartId == CartId);

            foreach (var cartItem in cartItems)
            {
                DB.CartItems.Delete(cartItem);
            }

            DB.SaveChanges();
        }

        public List<CartItem> Items()
        {
            return DB.CartItems.All().Where(
                cart => cart.CartId == CartId).ToList();
        }

        public int ItemsCount()
        {
            int? count = (from cartItems in DB.CartItems.All()
                          where cartItems.CartId == CartId
                          select (int?)cartItems.Quantity).Sum();

            return count ?? 0;
        }

        public void Migrate(string userName)
        {
            var shoppingCart = DB.CartItems.All().Where(
                c => c.CartId == CartId);

            foreach (var item in shoppingCart)
            {
                item.CartId = userName;
            }
            DB.SaveChanges();
        }

        private string GetCartId(HttpContextBase context)
        {
            if (context.Session[SessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[SessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[SessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[SessionKey].ToString();
        }

    }
}