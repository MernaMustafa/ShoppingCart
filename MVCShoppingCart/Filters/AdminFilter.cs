using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCShoppingCart
{
    public class AdminFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public AdminFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (_session.GetString("Type") != "admin")
            {
                context.Result = new RedirectResult("/Home/Error");

            }
        }
    }
}
