using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ShoppingCart.Repository
{
    public class CartItemRepository : BaseRepository<CartItem>
    {
        //private readonly ShoppingCartContext _db;
        public CartItemRepository(ShoppingCartContext db) : base(db)
        {
        }

        

        /*public async Task<int> AddCartItem(int userId, int itemId)
        {
            var existingCart = await _db.Carts.FirstOrDefaultAsync(c => c.UserId.Equals(userId));
            
            if (existingCart == null)
            {
                return 0;
            }
            var item = await _db.Items.FindAsync(itemId);
            if (item == null)
            {
                return 0;
            }
            //System.Diagnostics.Debug.WriteLine("--------itemId = " + itemId);
            var cartItem = await _db.CartItems.SingleOrDefaultAsync(c => c.CartId == existingCart.CartId 
                                                                    && c.ItemId == item.ItemId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ItemId = item.ItemId,
                    CartId = existingCart.CartId,
                    Item = item,
                    Quantity = 1,
                    Cart = existingCart
                };
                await _db.CartItems.AddAsync(cartItem);
                existingCart.CartItems.Add(cartItem);
                item.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            
            await _db.SaveChangesAsync();
            return cartItem.Id;

        }*/

        /*public async Task<int> UpdateCartItem(int cartItemId, int quantity)
        {
            
            CartItem existingCartItem = await _db.CartItems.FindAsync(cartItemId);
            if (existingCartItem != null)
            {
                if (quantity != 0)
                {
                    existingCartItem.Quantity = quantity;
                }
                else
                {
                    _db.CartItems.Remove(existingCartItem);
                }
            }
            return await _db.SaveChangesAsync();
        }*/

        
        /*public async Task<List<Item>> GetCartItemsByUserId(int userId)
        {
            return await(from ci in _db.CartItems
                         from i in _db.Items
                         from c in _db.Carts
                         where c.UserId == userId &&
                         c.CartId == ci.CartId &&
                         ci.ItemId == i.ItemId
                         select i).ToListAsync();
        }*/

        /*public async Task<List<CartItem>> GetCartItemsByUserId(int userId)
        {
            return await (from ci in _db.CartItems.Include(ci => ci.Item)
                          from c in _db.Carts
                          where c.UserId == userId &&
                          c.CartId == ci.CartId
                          select ci).ToListAsync();
        }*/

        /*public async Task<IEnumerable<CartItem>> GetAllCartItems()
        {
            return await _db.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetCartItemById(int cartItemId)
        {
            return await db.CartItems.FindAsync(cartItemId);
        }

        public async Task<int> RemoveCartItem(int cartItemId)
        {
            int result = 0;
            var cartItem = await db.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                result = await db.SaveChangesAsync();
            }
            return result;
        }*/
    }
}
