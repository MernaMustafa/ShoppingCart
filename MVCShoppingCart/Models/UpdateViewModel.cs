using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCShoppingCart.Models
{
    public class UpdateViewModel
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }

    }
}
