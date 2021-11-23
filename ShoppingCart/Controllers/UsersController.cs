using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Filters;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interfaces;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "admin")]
    [EnableCors("ApiCorsPolicy")]
    [AllowCrossSiteJson]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<User> _userRepository;
        public UsersController(IUnitOfWork unitOfWork, IBaseRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
       
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.ListAllAsync();
                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {

            try
            {
                var addedUser = await _userRepository.AddAsync(user);
                int result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    return Ok(addedUser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                _userRepository.DeleteAsync(user);
                int result = await  _unitOfWork.Complete();
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel authenticateModel)
        {
            var userAccount = await _userRepository.FindAsync(u => u.UserName == authenticateModel.UserName
                                                                   && u.Password == authenticateModel.Password);
            if (userAccount == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(userAccount);
        }
    }
}