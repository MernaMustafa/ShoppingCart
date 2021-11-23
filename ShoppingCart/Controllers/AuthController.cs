using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShoppingCart.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] AuthenticateModel authenticateModel)
        {
            UserInfoModel userInfoModel = await _authService.GetTokenAsync(authenticateModel);
            if (!userInfoModel.IsAuthenticated)
                return Unauthorized();
            return Ok(userInfoModel);
        }
    }
}
