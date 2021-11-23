﻿using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserInfoModel> GetTokenAsync(AuthenticateModel authenticateModel);
    }
}
