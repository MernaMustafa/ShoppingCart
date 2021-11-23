using ShoppingCart.Models;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingCartContext _db;
        public UnitOfWork(ShoppingCartContext db)
        {
            _db = db;
        }

        public async Task<int> Complete()
        {
            return await _db.SaveChangesAsync();
        }

        public async void Dispose()
        {
           await _db.DisposeAsync();
        }
    }
}
