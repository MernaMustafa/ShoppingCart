using ShoppingCart.Models;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> Complete();
    }
}
