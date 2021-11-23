using ShoppingCart.Models;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Interfaces;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoppingCart.Services
{
    public class CartItemService: ICartItemService
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Item> _itemRepository;
        public CartItemService(IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<Item> itemRepository, IBaseRepository<User> userRepository)
        {
            _cartItemRepository = cartItemRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }
    
        public async Task<CartItem> AddCartItem(OrderItemViewModel orderItemModel)
        {
            var user = await _userRepository.FindAsync(u => u.UserId == orderItemModel.UserId, new string[] { "Cart" });
            var cart = user.Cart;
            var item = await _itemRepository.GetByIdAsync(orderItemModel.ItemId);
            var cartItem =  await _cartItemRepository.FindAsync(c => c.CartId == cart.CartId
                                                                    && c.ItemId == item.ItemId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = item.ItemId,
                    CartId = cart.CartId,
                    Item = item,
                    Quantity = 1,
                    Cart = cart
                };
                await _cartItemRepository.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            return cartItem;
        }


        public async Task<string> UpdateQuantity(UpdateViewModel updateViewModel)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(updateViewModel.CartItemId);
            var quantity = updateViewModel.Quantity;
            if (quantity != 0)
            {
                cartItem.Quantity = quantity;
                return JsonConvert.SerializeObject(new { success = "updated" });
            }
            else
            {
                _cartItemRepository.DeleteAsync(cartItem);
                return JsonConvert.SerializeObject(new { success = "deleted" });
            }
        }

        public async Task<IEnumerable<CartItem>> GetByUserId(int userId)
        {
            var user = await _userRepository.FindAsync(u => (u.UserId == userId ), new string[] {"Cart"} );

            if (user.Cart == null)
            {
                user.Cart = new Cart
                {
                    UserId = user.UserId,
                    User = user,
                    CartItems = new List<CartItem>()
                };
            }
            var cartItems = await _cartItemRepository.FindAllAsync(ci => ci.CartId == user.Cart.CartId, new string []{ "Item" });
            return cartItems;
        }


    }
}
