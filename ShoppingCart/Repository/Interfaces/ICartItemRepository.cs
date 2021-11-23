using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interfaces;

namespace ShoppingCart.Repository
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
    }
}
