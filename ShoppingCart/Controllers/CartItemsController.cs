using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Filters;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "customer")]
    [EnableCors("ApiCorsPolicy")]
    [AllowCrossSiteJson]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemService _cartItemService;
        public CartItemsController(IUnitOfWork unitOfWork, ICartItemService cartItemService)
        {
            _unitOfWork = unitOfWork;
            _cartItemService = cartItemService;
        }

        // GET: api/CartItems/userid
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemsByUserId(int id)
        {
            
            try
            {
                var cartItems = await _cartItemService.GetByUserId(id);
                if (cartItems == null)
                {
                    return NotFound();
                }

                return Ok(cartItems);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Post: api/CartItems/userid
        //[Route("{id:int}")]
        [HttpPost]
        public async Task<IActionResult> PostCartItem([FromBody] OrderItemViewModel orderItemModel)
        {
            try
            {
                var cartItem = await _cartItemService.AddCartItem(orderItemModel);
                var result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    return Ok(cartItem);
                    
                    
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


        [HttpPost("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateViewModel updateViewModel)
        {

            try
            {

                string result =  await _cartItemService.UpdateQuantity(updateViewModel);
                var updated = await _unitOfWork.Complete();
                //Response.Headers.Add("result", result);
                if (updated > 0)
                {
                    return Ok(result);
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
    }
}