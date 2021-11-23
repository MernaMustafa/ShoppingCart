using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<CartItem> AddCartItem(OrderItemViewModel orderItemModel);
        Task<string> UpdateQuantity(UpdateViewModel updateViewModel);
        Task<IEnumerable<CartItem>> GetByUserId(int userId);
    }
}
