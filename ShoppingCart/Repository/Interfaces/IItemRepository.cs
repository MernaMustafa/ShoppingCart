using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Repository.Interfaces;

namespace ShoppingCart.Repository
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        //Task<int> AddOrUpdateItem(Item item);
       

    }
}
