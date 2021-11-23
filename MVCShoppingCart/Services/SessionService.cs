using Microsoft.AspNetCore.Http;
using MVCShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCShoppingCart.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _accessor;
        
        public SessionService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public void SetSession(UserInfoModel userInfo)
        {
            var _session = _accessor.HttpContext.Session;
            _session.SetString("Token", userInfo.Token);
            _session.SetString("UserID", userInfo.UserId.ToString());
            _session.SetString("Username", userInfo.Username);
            _session.SetString("Type", userInfo.Role);
        }

        public void ClearSession()
        {
            var _session = _accessor.HttpContext.Session;
            _session.Clear();
        }

        public string GetToken()
        {
             var _session = _accessor.HttpContext.Session;
            return _session.GetString("Token");
        }
    }
}
