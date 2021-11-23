using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCShoppingCart
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public AuthorizeActionFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
        
            if (_session.GetString("UserID") == null)
            {
                context.Result = new RedirectResult("/Home/Login");
            }
            else
            {

            }
        }

        
    }
}
