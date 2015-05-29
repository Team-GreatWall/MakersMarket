using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MakersMarket.Models.Domain
{
    public class Cart
    {
        private const string SessionKey = "CartId";

        private ApplicationDbContext DB = new ApplicationDbContext();
        private string CartId { get; set; }

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
            var cartItem = DB.CartItems.SingleOrDefault(
                c => c.CartId == CartId
                && c.ProductId == item.ID);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = item.ID,
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
            var cartItem = DB.CartItems.Single(
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
                    DB.CartItems.Remove(cartItem);
                }

                DB.SaveChanges();
            }
            return itemCount;
        }

        public void Clear()
        {
            var cartItems = DB.CartItems.Where(
                cart => cart.CartId == CartId);

            foreach (var cartItem in cartItems)
            {
                DB.CartItems.Remove(cartItem);
            }

            DB.SaveChanges();
        }

        public List<CartItem> Items()
        {
            return DB.CartItems.Where(
                cart => cart.CartId == CartId).ToList();
        }

        public int ItemsCount()
        {
            int? count = (from cartItems in DB.CartItems
                          where cartItems.CartId == CartId
                          select (int?)cartItems.Quantity).Sum();

            return count ?? 0;
        }

        public void Migrate(string userName)
        {
            var shoppingCart = DB.CartItems.Where(
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