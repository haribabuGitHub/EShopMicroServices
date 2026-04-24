using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } 

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

        public ShoppingCart(string username) { 
            UserName = username;
        }

        public ShoppingCart() { }
    }
}
