using ShoppingCart.Services.Interfaces;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ShoppingCart.Helpers;
using ShoppingCart.Repository.Interfaces;
using ShoppingCart.Models;
using System.Security.Claims;
using System.Text;

namespace ShoppingCart.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWT _jwt;
        private readonly IBaseRepository<User> _userRepository;

        public AuthService(IOptions<JWT> jwt, IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _jwt = jwt.Value;
        }
        public async Task<UserInfoModel> GetTokenAsync(AuthenticateModel authenticateModel)
        {
            var userInfoModel = new UserInfoModel();
            var user = await _userRepository.FindAsync(u => u.UserName == authenticateModel.UserName
                                                         && u.Password == authenticateModel.Password);
            if (user == null)
            {
                return userInfoModel;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Key);
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Type)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            userInfoModel.IsAuthenticated = true;
            userInfoModel.Token = tokenHandler.WriteToken(token);
            userInfoModel.UserId = user.UserId;
            userInfoModel.Username = user.UserName;
            userInfoModel.ExpiresOn = token.ValidTo;
            userInfoModel.Role = user.Type;

            return userInfoModel;
        }
    }
}
