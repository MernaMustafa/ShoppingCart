using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Cart
    {
        
        public int CartId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public IList<CartItem> CartItems { get; set; }
    }
}
