using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class UpdateViewModel
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }
    }
}
