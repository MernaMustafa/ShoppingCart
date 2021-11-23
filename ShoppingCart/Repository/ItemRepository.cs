using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
    public class ItemRepository : BaseRepository<Item>
    {
        public ItemRepository(ShoppingCartContext db) : base(db)
        {
        }
    }
}
