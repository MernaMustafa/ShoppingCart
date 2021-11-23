using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        
        public UserRepository(ShoppingCartContext db) : base(db)
        {
        }















        /*public async Task<int> AddUser(User user)
        {
            user.Cart = new Cart
            {
                UserId = user.UserId,
                User = user,
                CartItems = new List<CartItem>()
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<int> RemoveUser(int userId)
        {
            int result = 0;
            var user = await db.Users.FindAsync(userId);
            if (user != null)
            {
                db.Users.Remove(user);
                result = await db.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await db.Users.Include(u => u.Cart)
                                 .ThenInclude(c => c.CartItems)
                                 .ThenInclude(ci => ci.Item)
                                 .ToListAsync();

            // return await db.Users.ToListAsync();
        }*/

        /*public async Task<User> Authenticate(AuthenticateModel authenticateModel)
        {
            return await _db.Users.Where(u => u.UserName == authenticateModel.UserName  
                                           && u.Password == authenticateModel.Password).FirstOrDefaultAsync();
    
        }*/
    }
}
