using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCShoppingCart.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public IList<CartItem> CartItems { get; set; }
    }
}
